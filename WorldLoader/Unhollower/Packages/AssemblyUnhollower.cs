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

    public class AssemblyUnhollower
    {
        internal static GeneratorOptions opts;
        public static bool deobb { get; private set; } = false;
        public static bool Jsondeobb { get; private set; } = false;
        public static string DeobfuMap { get; private set; }
        public static string JsonDeobfuMap { get; private set; }

        internal bool Execute()
        {
            if (Directory.GetCurrentDirectory().ToLower().Contains("vrchat"))
            {
                Logs.Debug("Running VRC, Using DeObfu Map..");
                DeobfuMap = "https://raw.githubusercontent.com/WorldVRC/DeobfuscationMaps/main/VRChat/1275.csv.gz";
                JsonDeobfuMap = "https://raw.githubusercontent.com/WorldVRC/DeobfuscationMaps/main/VRChat/LocalRenameMap.json";
                Core.webClient.DownloadFile(DeobfuMap, "WorldLoader\\RenameMap.csv.gz"); // THIS NEEDS TO BE CHANGED TO NOT BE VRC ONLY
                Core.webClient.DownloadFile(JsonDeobfuMap, "WorldLoader\\LocalRenameMap.json"); // THIS NEEDS TO BE CHANGED TO NOT BE VRC ONLY
                deobb = true;
                Jsondeobb = true;
            }
            else Logs.Debug("No DeObfu Maps " + Directory.GetCurrentDirectory());
            opts = new GeneratorOptions
            {
                GameAssemblyPath = C.L.Config.GameAssemblyPath, // Path to GameAssembly.dll
                Source = Core.Cpp2ILOutputFolder, // List of Cpp2Il dummy assemblies loaded into Cecil
                OutputDir = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\WorldLoader\\UnhollowedAsm").FullName, // Path to which generate the assemblies
                UnityBaseLibsDir = Core.Dependencies.Destination, // Path to managed Unity core libraries (UnityEngine.dll etc)
                Parallel = false,
                PassthroughNames = C.L.Config.HollowerPassAllNames
            };
            if (deobb) {
                opts.ReadRenameMap("WorldLoader\\RenameMap.csv.gz");
                opts.DeObbJson = JsonConvert.DeserializeObject<List<Deobb>>(File.ReadAllText("WorldLoader\\LocalRenameMap.json"));
            }

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
