using System.IO;

namespace WorldLoader.Il2CppUnhollower.Packages
{
    internal class UnityDependencies : Models.PackageBase
    {
        internal UnityDependencies()
        {
            Name = nameof(UnityDependencies);
            Version = "2019.4.31";
            C.L.Config.UnityVersion = Version;
            URL = $"https://github.com/LavaGang/Unity-Runtime-Libraries/raw/master/{Version}.zip";
            Destination = Path.Combine(Core.LoaderFolderPath, Name);
            FilePath = Path.Combine(Core.LoaderFolderPath, $"{Name}_{Version}.zip");
        }

        internal override bool ShouldSetup()
            => string.IsNullOrEmpty(C.L.Config.UnityVersion)
            || !C.L.Config.UnityVersion.Equals(Version) || !Directory.Exists(Destination);
    }
}
