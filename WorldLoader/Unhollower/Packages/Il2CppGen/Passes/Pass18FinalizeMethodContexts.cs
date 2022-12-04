using WorldLoader.Il2CppGen.Internal;
using WorldLoader.Il2CppGen.Generator.Contexts;
using WorldLoader.Il2CppGen.Generator.Utils;

using Mono.Cecil;

namespace WorldLoader.Il2CppGen.Generator.Passes;

internal static class Pass18FinalizeMethodContexts
{
    internal static int TotalPotentiallyDeadMethods;

    internal static void DoPass(RewriteGlobalContext context)
    {
        var pdmNested0Caller = 0;
        var pdmNestedNZCaller = 0;
        var pdmTop0Caller = 0;
        var pdmTopNZCaller = 0;

        foreach (var assemblyContext in context.Assemblies)
            foreach (var typeContext in assemblyContext.Types)
                foreach (var methodContext in typeContext.Methods)
                {
                    methodContext.CtorPhase2();

                    if (Pass15GenerateMemberContexts.HasObfuscatedMethods)
                    {
                        var callerCount = 0;
                        if (Pass16ScanMethodRefs.MapOfCallers.TryGetValue(methodContext.Rva, out var callers))
                            callerCount = callers.Count;

                        methodContext.NewMethod.CustomAttributes.Add(
                            new CustomAttribute(assemblyContext.Imports.CallerCountAttributector.Value)
                            {
                                ConstructorArguments =
                                    {new CustomAttributeArgument(assemblyContext.Imports.Module.Int(), callerCount)}
                            });

                        if (!Pass15GenerateMemberContexts.HasObfuscatedMethods) continue;
                        if (!methodContext.UnmangledName.Contains("_PDM_")) continue;
                        TotalPotentiallyDeadMethods++;

                        var hasZeroCallers = callerCount == 0;
                        if (methodContext.DeclaringType.OriginalType.IsNested)
                        {
                            if (hasZeroCallers)
                                pdmNested0Caller++;
                            else
                                pdmNestedNZCaller++;
                        }
                        else
                        {
                            if (hasZeroCallers)
                                pdmTop0Caller++;
                            else
                                pdmTopNZCaller++;
                        }
                    }
                }

        if (Pass15GenerateMemberContexts.HasObfuscatedMethods)
        {
            Logger.Instance.LogTrace($"Dead method statistics: 0t={pdmTop0Caller} mt={pdmTopNZCaller} 0n={pdmNested0Caller} mn={pdmNestedNZCaller}");
        }
    }
}
