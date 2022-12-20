using Semver;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WorldLoader.Il2CppUnhollower.Packages
{
    internal class Cpp2IL : Models.ExecutablePackage // MelonLoader
    {
        internal string Name;
        internal string URL;
        internal string FilePath;
        internal string Destination;
        internal string Version = "";

        internal Cpp2IL()
        {
            if (string.IsNullOrEmpty(Version) || Version.Equals("0.0.0.0"))
                Version = "2022.1.0-pre-release.3";
            C.L.Config.DumperVersion = Version;
            Name = nameof(Cpp2IL);
            Destination = Path.Combine(Core.LoaderFolderPath, Name);
            OutputFolder = Path.Combine(Destination, "cpp2il_out");
            URL = $"https://github.com/SamboyCoding/{Name}/releases/download/{Version}/{Name}-{Version}-Windows-Netframework472.zip";
            ExeFilePath = Path.Combine(Destination, $"{Name}.exe");
            FilePath = Path.Combine(Core.LoaderFolderPath, $"{Name}_{Version}.zip");
        }

        internal override bool ShouldSetup() 
            => string.IsNullOrEmpty(C.L.Config.DumperVersion) 
            || !C.L.Config.DumperVersion.Equals(Version) || !Directory.Exists(Destination);

        internal override bool Execute()
        {
            if (SemVersion.Parse(Version) <= SemVersion.Parse("2022.0.999"))
                return ExecuteOld();
            return ExecuteNew();
        }

        private bool ExecuteNew()
        {
            if (Execute(new string[] {
                //MelonDebug.IsEnabled() ? "--verbose" : string.Empty,
                "--game-path",
                "\"" + Path.GetDirectoryName(C.L.Config.GameAssemblyPath) + "\"",
                "--exe-name",
                "\"" + Process.GetCurrentProcess().ProcessName + "\"",
                "--use-processor",
                "attributeinjector",
                "--output-as",
                "dummydll"

            }, false, new Dictionary<string, string>() {
                {"NO_COLOR", "1"}
            }, true))
                return true;

            return false;
        }

        private bool ExecuteOld()
        {
            if (Execute(new string[] {
                //MelonDebug.IsEnabled() ? "--verbose" : string.Empty,
                "--game-path",
                "\"" + Path.GetDirectoryName(C.L.Config.GameAssemblyPath) + "\"",
                "--exe-name",
                "\"" + Process.GetCurrentProcess().ProcessName + "\"",

                "--skip-analysis",
                "--skip-metadata-txts",
                "--disable-registration-prompts"

            }, false, new Dictionary<string, string>() {
                {"NO_COLOR", "1"}
            }, true))
                return true;

            return false;
        }
    }
}
