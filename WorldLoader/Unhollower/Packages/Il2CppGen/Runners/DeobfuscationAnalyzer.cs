using System;
using System.Linq;
using WorldLoader.Il2CppGen.Generator.Contexts;
using WorldLoader.Il2CppGen.Generator.MetadataAccess;
using WorldLoader.Il2CppGen.Generator.Passes;
using WorldLoader.Il2CppGen.Generator.Utils;

namespace WorldLoader.Il2CppGen.Generator.Runners;

public static class DeobfuscationAnalyzer
{
    public static Il2CppGenGenerator AddDeobfuscationAnalyzer(this Il2CppGenGenerator gen)
    {
        return gen.AddRunner<DeobfuscationAnalyzerRunner>();
    }
}

internal class DeobfuscationAnalyzerRunner : IRunner
{
    public void Dispose() { }

    public void Run(GeneratorOptions options)
    {
        RewriteGlobalContext rewriteContext;
        IIl2CppMetadataAccess inputAssemblies;
        using (new TimingCookie("Reading assemblies"))
        {
            inputAssemblies = new CecilMetadataAccess(options.Source);
        }

        using (new TimingCookie("Creating assembly contexts"))
        {
            rewriteContext = new RewriteGlobalContext(options, inputAssemblies, NullMetadataAccess.Instance);
        }

        for (var chars = 1; chars <= 3; chars++)
            for (var uniq = 3; uniq <= 15; uniq++)
            {
                options.TypeDeobfuscationCharsPerUniquifier = chars;
                options.TypeDeobfuscationMaxUniquifiers = uniq;

                rewriteContext.RenamedTypes.Clear();
                rewriteContext.RenameGroups.Clear();

                Pass05CreateRenameGroups.DoPass(rewriteContext);

                var uniqueTypes = rewriteContext.RenameGroups.Values.Count(it => it.Count == 1);
                var nonUniqueTypes = rewriteContext.RenameGroups.Values.Count(it => it.Count > 1);

                // Ensure the output is written to stdout
                Console.WriteLine($"Chars=\t{chars}\tMaxU=\t{uniq}\tUniq=\t{uniqueTypes}\tNonUniq=\t{nonUniqueTypes}");
            }
    }
}
