using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WorldLoader.Attributes;
using WorldLoader.HookUtils;
using WorldLoader.Il2CppGen.Internal.Extensions;
using WorldLoader.Plugins;

namespace WorldLoader.ModulesLibs.Managers;

public sealed class PluginManager
{
	public List<WLPlugin> LoadedPlugins { get; private set; } = new List<WLPlugin>();

	internal void LoadPlugins()
	{
		Logs.Log(ConsoleColor.DarkGray, "==================================- Plugins -==================================");
		Logs.Log();
		foreach (string text in Directory.GetFiles(PathDataInfo.PluginsPath))
		{
			try {
				if (Path.GetExtension(text) == ".dll") {
					Assembly assembly = null;
					try {
						assembly = Assembly.Load(File.ReadAllBytes(text));
					}
					catch (Exception ex) {
						Logs.Error($"Error loading \"{Path.GetFileName(text)}\". Are you sure this is a valid assembly?\n", ex);
					}

					if (assembly == null) {
						Logs.Log($"[Error] {text} Plugin Assemblys Are Null!");
						continue;
					}

					WLPlugin mod = null;

					try
					{
						try {
							mod = assembly.GetTypesSafe().Where(o => o.IsSubclassOf(typeof(WLPlugin))).Select(a => (WLPlugin)Activator.CreateInstance(a)).FirstOrDefault();
						}
						catch (ReflectionTypeLoadException ex) {
							StringBuilder sb = new StringBuilder();
							foreach (Exception exSub in ex.LoaderExceptions) {
								sb.AppendLine(exSub.Message);
								FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
								if (exFileNotFound != null) {
									if (!string.IsNullOrEmpty(exFileNotFound.FusionLog)) {
										sb.AppendLine("Fusion Log:");
										sb.AppendLine(exFileNotFound.FusionLog);
									}
								}
								sb.AppendLine();
							}
							Logs.Error(sb.ToString());
						}
					}
					catch (Exception e) {
						Logs.Error($"[Error] Plugin Was Not Found Inside Of Dll {text}!", e);
						continue;
					}
					PluginAttribute PluginAttributes = null;
					try {
						PluginAttributes = mod.GetType().GetCustomAttributes(typeof(PluginAttribute), true).FirstOrDefault<object>() as PluginAttribute;
					}
					catch (Exception e) {
						Logs.Error("Unable To get Main Type of the Mod for Attributes!", e);
					}

					if (PluginAttributes != null){
						WLPlugin Plugin = mod;
						LoadedPlugins.Add(Plugin);
						Plugin.Initialize(PluginAttributes, this);

						Logs.Log(Plugin.ModColor, $"======= [{Plugin.Name}] - {Plugin.Version} =======");
						Logs.Log(Plugin.ModColor, $"   Made By: {Plugin.Author}");
						if (!string.IsNullOrEmpty(Plugin.Link))
							Logs.Log(Plugin.ModColor, $"   Link By: {Plugin.Link}");
						Logs.Log();
					}
					else {
						Logs.Error("Attributes Are Null! - " + text);
					}
				}
			}
			catch (Exception e) {
				Logs.Error("UnKnown Error Loading Plugin - " + text, e);
			}
		}
		Logs.Log(ConsoleColor.DarkGray, "==================================- Plugins -==================================");
	}
}