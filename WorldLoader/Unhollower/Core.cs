using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using WorldLoader.Discord;
using WorldLoader.Il2CppUnhollower.Packages;
using WorldLoader.Il2CppUnhollower.Packages.Models;
using WorldLoader.HookUtils;
using WorldLoader.Utils;
using Runtime.Il2cpp;

namespace WorldLoader.Il2CppUnhollower
{
    internal class Core
    {
        internal static List<AssemblyDefinition> Cpp2ILOutputFolder { get; set; } = new List<AssemblyDefinition>();

        internal static WebClient webClient = null;
        internal static Packages.Models.ExecutablePackage dumper = null;
        internal static AssemblyUnhollower assemblyunhollower = null;
        internal static UnityDependencies Dependencies = null;
        internal static string LoaderFolderPath = null;
        internal static bool AssemblyGenerationNeeded = false;

        internal static void OnInitialize()
        {
            webClient = new();
            if (string.IsNullOrEmpty(C.L.Config.GameAssemblyPath))
                if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "GameAssembly.dll"))) {
                    MessageBox.Show("Unable to find the GameAssembly! (you can try manually adding it in the config)", "Fatal Error");
                    return;
                }
                else C.L.Config.GameAssemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "GameAssembly.dll");
            
            LoaderFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "WorldLoader");
            webClient.Headers.Add("User-Agent", "WorldLoader v0.5.4");

            try
            {
                switch (Run())
                {
                    default:
                        break;
                    case 0:
                        Logs.Log("Up To Date!");
                        break;
                    case 1:
                        Logs.Error("Failed During SetUp!");
                        break;
                    case 2:
                        Logs.Error("Failed During Dumper Execution!");
                        break;
                    case 3:
                        Logs.Error("Failed During AssemblyUnhollower Execution!");
                        break;
                }
            } catch (Exception e) {
                Logs.Error("Error During AssemblyGeneration Stuff", e);
            }
            Internal_Utils.RunInTry(() => {
                WorldLoader._AssemblyResolveManager = new();
            });
        }

        internal static float Run(bool ForceRegen = false)
        {
            dumper = new Cpp2IL();
            assemblyunhollower = new AssemblyUnhollower();
            Dependencies = new UnityDependencies();
            //Logs.Log($"Using Deobfuscation Regex = {(string.IsNullOrEmpty(DeobfuscationRegex.Regex) ? "null" : DeobfuscationRegex.Regex)}", "Assembly Generation");

            if (!dumper.Setup()
                || !Dependencies.Setup())
                return 1;

            //DeobfuscationRegex.Setup();

            string CurrentGameAssemblyHash = FileHandler.Hash(C.L.Config.GameAssemblyPath);
            Logs.Log("Getting GameAssembly...", "Assembly Generation");
            Logs.Log($"Last GameAssembly Hash: {C.L.Config.GameAssemblyHash}", "Assembly Generation");
            Logs.Log($"Current GameAssembly Hash: {CurrentGameAssemblyHash}", "Assembly Generation");

            if (string.IsNullOrEmpty(C.L.Config.GameAssemblyHash)
                    || !C.L.Config.GameAssemblyHash.Equals(CurrentGameAssemblyHash))
                AssemblyGenerationNeeded = true;

            if (!AssemblyGenerationNeeded && !ForceRegen) {
                if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "WorldLoader", "LocalRenameMap.json")))
                    webClient.DownloadFile("https://raw.githubusercontent.com/WorldVRC/DeobfuscationMaps/main/VRChat/LocalRenameMap.json", "WorldLoader\\LocalRenameMap.json"); // THIS NEEDS TO BE CHANGED TO NOT BE VRC ONLY
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "WorldLoader", "LocalRenameMap.json"))) {
                    long CurrentDeObbMapHash = new FileInfo("WorldLoader\\LocalRenameMap.json").Length;
                    Logs.Log("Getting DeObbMap...", "Assembly Generation");
                    Logs.Log($"Last DeObbMap ByteSize: {C.L.Config.DeObbMapHash}", "Assembly Generation");
                    Logs.Log($"Current DeObbMap ByteSize: {CurrentDeObbMapHash}", "Assembly Generation");
                    if (CurrentDeObbMapHash != C.L.Config.DeObbMapHash) {
                        Cpp2ILOutputFolder = Cpp2IL.LoadAssembliesFrom(Directory.CreateDirectory(dumper.OutputFolder));
                        Logs.Log("Warning! Local Deobfu Sizes are Incorrect!", "Deobfuscation");
                        try {
                            assemblyunhollower.Execute();
                        }
                        finally {
                            C.L.Config.DeObbMapHash = new FileInfo("WorldLoader\\LocalRenameMap.json").Length;
                            C.L.Save();
                        }
                    }
                }
                Logs.Log("Assembly is up to date! Regeneration Not Needed.", "Assembly Generation");
                return 0;
            }

            Logs.Log("Generation Needed!", "Assembly Generation");
            Update.UpdateRPC("Starting Generation...");

            if (!dumper.Execute())
                return 2;
            Update.UpdateRPC("UnHollowing Assemblys...");

            if (!assemblyunhollower.Execute())
                return 3;
            Logs.Log("Successful!", "Assembly Generation");
            C.L.Config.GameAssemblyHash = CurrentGameAssemblyHash;
            C.L.Config.DeObbMapHash = new FileInfo("WorldLoader\\LocalRenameMap.json").Length;
            C.L.Save();

            return 0;
        }
    }
}