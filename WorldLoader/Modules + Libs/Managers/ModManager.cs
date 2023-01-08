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
using WorldLoader.Mods;

namespace WorldLoader.ModulesLibs.Managers;

public sealed class ModManager
{
	private static List<UnityMod> _Mods { get; } = new();

	public static Dictionary<UnityMod, (string, FileInfo)> Mods = new();

	public static UnityMod LoadMod(byte[] modBytes, bool InvokeOnInject = true, bool LogInfo = true) => LoadMod(Assembly.Load(modBytes), InvokeOnInject, LogInfo);

	public static UnityMod LoadMod(Assembly assembly, bool InvokeOnInject = true, bool LogInfo = true) {
		FileInfo fileinfo = new(assembly.Location);
		if (assembly == null) {
			Logs.Log("[Error] Mods Assemblys Are Null!");
			return null;
		}

		UnityMod vrMod = null;
		Type type = null;

		try {
			vrMod = assembly.GetTypesSafe().Where(o => o
						.IsSubclassOf(typeof(UnityMod)))
							.Select(a =>
							(UnityMod)Activator
							.CreateInstance(a))
							.FirstOrDefault();
		}
		catch (Exception e)
		{
			Logs.Error($"[Error] Mod Was Not Found Inside Of Dll {assembly.Location}!", e);
			return null;
		}
		type = vrMod.GetType();

		ModAttribute ModAttributes;
		if ((ModAttributes = type.GetCustomAttributes(typeof(ModAttribute), true).FirstOrDefault<object>() as ModAttribute) != null)
		{
			_Mods.Add(vrMod);
			Mods.Add(vrMod, (fileinfo.FullName, fileinfo));
			vrMod.Initialize(ModAttributes);
			WorldLoader.Menu.flatComboBox2.Items.Add(vrMod.Name);


			if (LogInfo)
			{
				Logs.Log(vrMod.ModColor, $"======= [{vrMod.Name}] - {vrMod.Version} =======");
				Logs.Log(vrMod.ModColor, $"   Made By: {vrMod.Author}");
				if (!string.IsNullOrEmpty(vrMod.Link))
					Logs.Log(vrMod.ModColor, $"   Link By: {vrMod.Link}");
				Logs.Log();
			}
			if (InvokeOnInject) vrMod.OnInject();

			return vrMod;
		}
		else
		{
			Logs.Error("File Missing Attributes! - " + assembly.Location);
			if (C.L.Config.Debug)
			{
				Logs.Debug("Trying to load Anyways...");
				var title = type.Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).SingleOrDefault<object>() as AssemblyTitleAttribute;
				var autor = type.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).SingleOrDefault<object>() as AssemblyCompanyAttribute;
				ModAttributes = new(title.Title, "Unknown", autor.Company);

				vrMod.Initialize(ModAttributes);
				WorldLoader.Menu.flatComboBox2.Items.Add(vrMod.Name);


				if (LogInfo)
				{
					Logs.Log(vrMod.ModColor, $"======= [{vrMod.Name}] - {vrMod.Version} =======");
					Logs.Log(vrMod.ModColor, $"   Made By: {vrMod.Author}");
					if (!string.IsNullOrEmpty(vrMod.Link))
						Logs.Log(vrMod.ModColor, $"   Link By: {vrMod.Link}");
					Logs.Log();
				}
				if (InvokeOnInject) vrMod.OnInject();
				return vrMod;
			}
		}
		return null;
	}

	public static UnityMod LoadMod(string FilePath, bool InvokeOnInject = true, bool LogInfo = true) {
		if (Path.GetExtension(FilePath) == ".dll") {
			Assembly assembly = null;
			try {
				assembly = Assembly.Load(File.ReadAllBytes(FilePath));
			}
			catch (Exception ex) {
				Logs.Error($"Error loading \"{Path.GetFileName(FilePath)}\". Are you sure this is a valid assembly?\n", ex);
			}
			LoadMod(assembly, InvokeOnInject, LogInfo);
		}
		else Logs.Error("FILE NOT DLL!", new FormatException());
		return null;
	}

	internal static Dictionary<UnityMod, (string, FileInfo)> FindMods()
	{

		Logs.Log(ConsoleColor.DarkGray, "==================================- Mods -==================================");
		Logs.Log();
		foreach (string text in Directory.GetFiles(PathDataInfo.ModPath))
		{
			try
			{
				if (Path.GetExtension(text) == ".dll")
				{
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

					UnityMod vrMod = null;
					Type type = null;

					try {
						vrMod = assembly.GetTypesSafe().Where(o => o
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
					type = vrMod.GetType();


					ModAttribute ModAttributes;
					if ((ModAttributes = type.GetCustomAttributes(typeof(ModAttribute), true).FirstOrDefault<object>() as ModAttribute) != null) {
						_Mods.Add(vrMod);
						Mods.Add(vrMod, (text, new FileInfo(text)));
						vrMod.Initialize(ModAttributes);
						WorldLoader.Menu.flatComboBox2.Items.Add(vrMod.Name);

						Logs.Log(vrMod.ModColor, $"======= [{vrMod.Name}] - {vrMod.Version} =======");
						Logs.Log(vrMod.ModColor, $"   Made By: {vrMod.Author}");
						if (!string.IsNullOrEmpty(vrMod.Link))
							Logs.Log(vrMod.ModColor, $"   Link By: {vrMod.Link}");
						Logs.Log();
					}
					else {
						Logs.Error("File Missing Attributes! - " + text);
						if (C.L.Config.Debug) {
							Logs.Debug("Trying to load Anyways...");
							var title = type.Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).SingleOrDefault<object>() as AssemblyTitleAttribute;
							var autor = type.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).SingleOrDefault<object>() as AssemblyCompanyAttribute;
							ModAttributes = new(title.Title, "Unknown", autor.Company);

							vrMod.Initialize(ModAttributes);
							WorldLoader.Menu.flatComboBox2.Items.Add(vrMod.Name);


							Logs.Log(vrMod.ModColor, $"======= [{vrMod.Name}] - {vrMod.Version} =======");
							Logs.Log(vrMod.ModColor, $"   Made By: {vrMod.Author}");
							if (!string.IsNullOrEmpty(vrMod.Link))
								Logs.Log(vrMod.ModColor, $"   Link By: {vrMod.Link}");
							Logs.Log();
						}
					}


				}
			}
			catch (Exception e)	{
				Logs.Error("UnKnown Error Loading Mod - " + text, e);
			}
		}
		Logs.Log(ConsoleColor.DarkGray, "==================================- Mods -==================================");
		return Mods;
	}

	public static void UnloadMod(UnityMod Mod)
	{
		if (_Mods.Contains(Mod)) {
			if (!Mod.AllowUnloading) {
				Logs.Warn("Mod Doesn't Support Unloading!"); return;
			}
			_Mods.Remove(Mod);
			Mods.Remove(Mod);
			Logs.Log($"{Mod} unloaded!");
		}
	}

	public static void UnloadMod(string Mod) {
		foreach (var VRCMod in _Mods)
			if (VRCMod.Name == Mod) {
				VRCMod.Unload();
				break;
			}
	}
}
