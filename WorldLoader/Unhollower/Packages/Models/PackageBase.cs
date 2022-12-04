using System.IO;
using WorldLoader.HookUtils;

namespace WorldLoader.Il2CppUnhollower.Packages.Models
{
    internal class PackageBase
    {
        internal string Name;
        internal string URL;
        internal string FilePath;
        internal string Destination;
        internal string Version = "";

        internal virtual bool ShouldSetup() => true;
        internal bool Setup()
        {
            if (string.IsNullOrEmpty(Version) || string.IsNullOrEmpty(URL))
                return true;

            if (!ShouldSetup())
            {
                Logs.Log($"{Name} is up to date.");
                return true;
            }

            Core.AssemblyGenerationNeeded = true;

            if (!File.Exists(FilePath)) 
            {
                Logs.Log($"Downloading {Name}...");
                if (!FileHandler.Download(URL, FilePath))
                {
                    ThrowInternalFailure($"Failed to Download {Name}!");
                    return false;
                }
            }

            Logs.Log($"Processing {Name}...");
            if (!FileHandler.Process(FilePath, Destination))
            {
                ThrowInternalFailure($"Failed to Process {Name}!");
                return false;
            }

            C.L.Save();
            return true;
        }

        internal static void ThrowInternalFailure(string text) => Logs.Error(text);
    }
}
