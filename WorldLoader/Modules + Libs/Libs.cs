using System;
using System.IO;
using System.Reflection;
using WorldLoader.HookUtils;

namespace WorldLoader
{
    internal class Libs
    {
		internal static void FindLibs(string path = "Libs\\")
		{
			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(path);
				Logs.Error("Libs folder does not exist.");
				return;
			}
			foreach (string text in Directory.GetFiles(path))
					if (Path.GetExtension(text) == ".dll") {
						Assembly assembly = null;

						try {
							assembly = Assembly.Load(File.ReadAllBytes(text));
						}
						catch (Exception ex) {
							Logs.Error($"Error loading \"{Path.GetFileName(text)}\". Are you sure this is a valid assembly?\n", ex);
						}

						if (assembly == null) {
							Logs.Log("[Error] - " + text);
							break;
						}
					}
		}
	}
}
