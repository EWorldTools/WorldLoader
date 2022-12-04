using WorldLoader.Il2CppGen.Generator;
using WorldLoader.Il2CppGen.Generator.Runners;
using WorldLoader.Il2CppGen.Json;
using Mono.Cecil;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace WorldLoader.Il2CppUnhollower.Packages
{
    internal class AssemblyUnhollower : Models.ExecutablePackage
    {
        internal static GeneratorOptions opts;

        internal AssemblyUnhollower()
        {
            //            if (string.IsNullOrEmpty(Version) || Version.Equals("0.0.0.0"))
            //                Version = "0.4.17.1";
            //            C.L.Config.UnhollowerVersion = Version;
            //            Name = nameof(Il2CppAssemblyUnhollower);
            //            URL = $"https://github.com/knah/{Name}/releases/download/v{Version}/{Name}.{Version}.zip";
            //            Destination = Path.Combine(Core.LoaderFolderPath, Name);
            //            OutputFolder = Core.BasePath;
            //            ExeFilePath = Path.Combine(Core.LoaderFolderPath, $"{Name}\\AssemblyUnhollower.exe");
            //            FilePath = Path.Combine(Core.LoaderFolderPath, $"{Name}_{Version}.zip");
            Core.webClient.DownloadFile("https://raw.githubusercontent.com/Hacker1254/Deobfuscation-Maps/main/DeobbMaps.json", "WorldLoader\\DeobbMaps.json");
        }

        internal override bool ShouldSetup()
            => false;

        internal override bool Execute()
        {
            Core.webClient.DownloadFile("https://raw.githubusercontent.com/Hacker1254/Deobfuscation-Maps/main/DeobbMaps.json", "WorldLoader\\DeobbMaps.json");

            opts = new GeneratorOptions
            {
                GameAssemblyPath = C.L.Config.GameAssemblyPath, // Path to GameAssembly.dll
                Source = Core.Cpp2ILOutputFolder, // List of Cpp2Il dummy assemblies loaded into Cecil
                OutputDir = Directory.GetCurrentDirectory(), // Path to which generate the assemblies
                UnityBaseLibsDir = Core.Dependencies.Destination, // Path to managed Unity core libraries (UnityEngine.dll etc)
                Parallel = false,
                PassthroughNames = C.L.Config.HollowerPassAllNames
            };
            if (File.Exists("WorldLoader\\DeobbMaps.json"))
                opts.DeObbJson = JsonConvert.DeserializeObject<List<Deobb>>(File.ReadAllText("WorldLoader\\DeobbMaps.json"));
            opts.AdditionalAssembliesBlacklist.Add("ICSharpCode");
            opts.AdditionalAssembliesBlacklist.Add("Newtonsoft");
            opts.AdditionalAssembliesBlacklist.Add("TinyJson");
            opts.AdditionalAssembliesBlacklist.Add("Valve.Newtonsoft");
            opts.AdditionalAssembliesBlacklist.Add("Newtonsoft.Json");
            
            Il2CppGenGenerator.Create(opts)
                                  .AddAssemblyGenerator()
                                  .Run();
            return true; 

            //return false;
        }
    }
}
