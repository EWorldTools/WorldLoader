using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WorldLoader.Attributes;
using WorldLoader.HookUtils;

namespace WorldLoader.Mods;

public sealed class ModManager
{
	private List<UnityMod> _Mods { get; } = new List<UnityMod>();

	public ReadOnlyCollection<UnityMod> Mods
	{
		get {
			return _Mods.AsReadOnly();
		}
	}

	public UnityMod LoadMod(string FilePath, bool InvokeOnInject = true, bool LogInfo = true) {
		if (Path.GetExtension(FilePath) == ".dll") {
			Assembly assembly = null;
			try {
				assembly = Assembly.Load(File.ReadAllBytes(FilePath));
			}
			catch (Exception ex) {
				Logs.Error($"Error loading \"{Path.GetFileName(FilePath)}\". Are you sure this is a valid assembly?\n", ex);
			}

			if (assembly == null) {
				Logs.Log("[Error] Mods Assemblys Are Null!");
				return null;
			}

			UnityMod mod = null;
			Type type = null;

			try {
				mod = assembly.GetTypes().Where(o => o
							.IsSubclassOf(typeof(UnityMod)))
								.Select(a =>
								(UnityMod)Activator
								.CreateInstance(a))
								.FirstOrDefault();
			}
			catch (Exception e) {
				Logs.Error($"[Error] Mod Was Not Found Inside Of Dll {FilePath}!", e);
				return null;
			}
			type = mod.GetType();

			ModAttribute ModAttributes;
			if ((ModAttributes = type.GetCustomAttributes(typeof(ModAttribute), true).FirstOrDefault<object>() as ModAttribute) != null) {
				UnityMod vrMod = mod;
				_Mods.Add(vrMod);
				vrMod.Initialize(ModAttributes, this);
				WorldLoader.Menu.flatComboBox2.Items.Add(vrMod.Name);


				if (LogInfo) {
					Logs.Log(vrMod.ModColor, $"======= [{vrMod.Name}] - {vrMod.Version} =======");
					Logs.Log(vrMod.ModColor, $"   Made By: {vrMod.Author}");
					if (!string.IsNullOrEmpty(vrMod.Link))
						Logs.Log(vrMod.ModColor, $"   Link By: {vrMod.Link}");
					Logs.Log();
				}
				if (InvokeOnInject) vrMod.OnInject();

				return vrMod;
			}
			else {
				Logs.Error("File Missing Attributes! - " + FilePath);
			}
		}
		else Logs.Error("FILE NOT DLL!", new FormatException());
		return null;
	}

	internal ReadOnlyCollection<UnityMod> FindMods()
	{
		ReadOnlyCollection<UnityMod> Collection;
		string path = "Mods\\";
		{
			Logs.Log(ConsoleColor.DarkGray, "==================================- Mods -==================================");
			Logs.Log();
			foreach (string text in Directory.GetFiles(path)) {
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
							Logs.Log($"[Error] {text} Mod Assemblys Are Null!");
							continue;
						}

						UnityMod mod = null;
						Type type = null;

						try {
							mod = assembly.GetTypes().Where(o => o
							.IsSubclassOf(typeof(UnityMod)))
								.Select(a =>
								(UnityMod)Activator
								.CreateInstance(a))
								.FirstOrDefault();
						}
						catch (Exception e)
						{
							Logs.Error($"[Error] Mod Was Not Found Inside Of Dll {text}!", e);
							continue;
						}
						type = mod.GetType();


						ModAttribute ModAttributes;
						if ((ModAttributes = type.GetCustomAttributes(typeof(ModAttribute), true).FirstOrDefault<object>() as ModAttribute) != null) {
							UnityMod vrMod = mod;
							_Mods.Add(vrMod);
							vrMod.Initialize(ModAttributes, this);
							WorldLoader.Menu.flatComboBox2.Items.Add(vrMod.Name);

							Logs.Log(vrMod.ModColor, $"======= [{vrMod.Name}] - {vrMod.Version} =======");
							Logs.Log(vrMod.ModColor, $"   Made By: {vrMod.Author}");
							if (!string.IsNullOrEmpty(vrMod.Link))
								Logs.Log(vrMod.ModColor, $"   Link By: {vrMod.Link}");
							Logs.Log();
						}
						else {
							Logs.Error("File Missing Attributes! - " + text);
						}

					}
				}
				catch (Exception e) {
					Logs.Error("UnKnown Error Loading Mod - " + text, e);
				}
			}
			Logs.Log(ConsoleColor.DarkGray, "==================================- Mods -==================================");
			Collection = Mods;
		}
		return Collection;
	}

	public void UnloadMod(UnityMod Mod)
	{
		if (_Mods.Contains(Mod)) {
			_Mods.Remove(Mod);
			Logs.Log($"{Mod} unloaded!");
		}
	}

	public void UnloadMod(string Mod) {
		foreach (var VRCMod in _Mods)
			if (VRCMod.Name == Mod)
				VRCMod.Unload();
	}
}
