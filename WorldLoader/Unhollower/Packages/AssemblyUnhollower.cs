using WorldLoader.Il2CppGen.Generator;
using WorldLoader.Il2CppGen.Generator.Runners;
using WorldLoader.Il2CppGen.Json;
using Mono.Cecil;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using WorldLoader.HookUtils;

namespace WorldLoader.Il2CppUnhollower.Packages
{

    internal class AssemblyUnhollower
    {
        internal static GeneratorOptions opts;
        internal static bool deobb = false;

        internal bool Execute()
        {
            if (Directory.GetCurrentDirectory().ToLower().Contains("vrchat"))
            {
                Logs.Debug("Running VRC, Using DeObfu Map..");
                Core.webClient.DownloadFile("https://raw.githubusercontent.com/WorldVRC/DeobfuscationMaps/main/VRChat/1275.csv.gz", "WorldLoader\\RenameMap.csv.gz"); // THIS NEEDS TO BE CHANGED TO NOT BE VRC ONLY
                deobb = true;
            }
            else Logs.Debug("No DeObfu Maps " + Directory.GetCurrentDirectory());
            opts = new GeneratorOptions
            {
                GameAssemblyPath = C.L.Config.GameAssemblyPath, // Path to GameAssembly.dll
                Source = Core.Cpp2ILOutputFolder, // List of Cpp2Il dummy assemblies loaded into Cecil
                OutputDir = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\WorldLoader\\UnhollowedAsm").FullName, // Path to which generate the assemblies
                UnityBaseLibsDir = Core.Dependencies.Destination, // Path to managed Unity core libraries (UnityEngine.dll etc)
                Parallel = false,
                PassthroughNames = C.L.Config.HollowerPassAllNames,
                //TypeDeobfuscationCharsPerUniquifier = 999,// Remove Limit
                //TypeDeobfuscationMaxUniquifiers = 99, //     ^
            };
            if (deobb)
                opts.ReadRenameMap("WorldLoader\\RenameMap.csv.gz");

            opts.AdditionalAssembliesBlacklist.Add("ICSharpCode");
            opts.AdditionalAssembliesBlacklist.Add("Newtonsoft");
            opts.AdditionalAssembliesBlacklist.Add("TinyJson");
            opts.AdditionalAssembliesBlacklist.Add("Valve.Newtonsoft");
            opts.AdditionalAssembliesBlacklist.Add("Newtonsoft.Json");
            
            Il2CppGenGenerator.Create(opts)
                                  .AddAssemblyGenerator()
                                  .Run();
            return true; 
        }
    }
}
