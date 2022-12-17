using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using WorldLoader.Il2CppGen.Json;
using Mono.Cecil;

namespace WorldLoader.Il2CppGen.Generator;

public class GeneratorOptions
{
    internal List<AssemblyDefinition>? Source { get; set; }
    internal string? OutputDir { get; set; }

    internal List<Deobb>? DeObbJson { get; set; }

    internal string? UnityBaseLibsDir { get; set; }
    internal List<string> AdditionalAssembliesBlacklist { get; } = new();
    internal int TypeDeobfuscationCharsPerUniquifier { get; set; } = 0;
    internal int TypeDeobfuscationMaxUniquifiers { get; set; } = 0;
    internal string? GameAssemblyPath { get; set; }
    internal bool Verbose { get; set; }
    internal bool NoXrefCache { get; set; }
    internal Regex? ObfuscatedNamesRegex { get; set; }
    internal Dictionary<string, string> RenameMap { get; } = new();
    internal bool PassthroughNames { get; set; }
    internal bool Parallel { get; set; } = true;

    internal PrefixMode Il2CppPrefixMode { get; set; } = PrefixMode.OptIn;
    internal HashSet<string> NamespacesAndAssembliesToPrefix { get; } =
        new() { "System", "mscorlib", "Microsoft", "Mono", "I18N" };
    internal HashSet<string> NamespacesAndAssembliesToNotPrefix { get; } =
        new() { "Assembly-CSharp", "Unity" };

    internal List<string> DeobfuscationGenerationAssemblies { get; } = new();
    internal string? DeobfuscationNewAssembliesPath { get; set; }

    /// <summary>
    ///     Reads a rename map from the specified name into the specified instance of options
    /// </summary>
    internal void ReadRenameMap(string fileName)
    {
        using var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        ReadRenameMap(fileStream, fileName.EndsWith(".gz"));
    }

    /// <summary>
    ///     Reads a rename map from the specified name into the specified instance of options.
    ///     The stream is not closed by this method.
    /// </summary>
    internal void ReadRenameMap(Stream fileStream, bool isGzip)
    {
        if (isGzip)
        {
            using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress, true);
            ReadRenameMap(gzipStream, false);
            return;
        }

        using var reader = new StreamReader(fileStream, Encoding.UTF8, false, 65536, true);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrEmpty(line) || line.StartsWith("#")) continue;
            var split = line.Split(';');
            if (split.Length < 2) continue;
            RenameMap[split[0]] = split[1];
        }
    }

    internal enum PrefixMode
    {
        /// <summary>
        ///     Only specified namespaces and assemblies will be renamed.
        /// </summary>
        OptIn,
        /// <summary>
        ///     Only specified namespaces and assemblies will not be renamed.
        /// </summary>
        OptOut
    }
}
