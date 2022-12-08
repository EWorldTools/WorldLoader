using System;
using System.Runtime.InteropServices;
using WorldLoader.Mods;
using WorldLoader.Plugins;
using WorldLoader.HookUtils;
using WorldLoader.Discord;
using WorldLoader.Init;
using WorldLoader.Il2CppGen;
using WorldLoader.Utils;
using Il2CppGen;
using WorldLoader.DataClasses;
using Il2CppGen.Runtime.Injection;
using UnityEngine;

namespace WorldLoader
{
    internal partial class WorldLoader
	{
		public static readonly bool IsAuth = LoggedIn;
		private static bool LoggedIn = false;
		internal static bool MonoBhvMade = false;
		private int count;

		public static WorldLoader Self { get; set; }
		internal static ModManager _ModManager { get; set; }
		internal static PluginManager _PluginManager { get; set; }
		internal static LoaderMenu Menu { get; set; }
		internal static HarmonyLib.Harmony HarmonyInstance { get; set; }

		protected internal static void Login() {
            Internal_Utils.RunInTry(Load, null, true);
            Internal_Utils.RunInTry(Preint.Start);

            Logs.Log(ConsoleColor.DarkCyan, "Logging In And Checking Tags...");
            Logs.Debug(" -=========================== Debug Mode On! ===========================- ", ConsoleColor.Gray);
            Internal_Utils.RunInTry(Il2CppUnhollower.Core.OnInitialize);
            Self.Awake();
            Internal_Utils.RunInTry(Interface.StartLoadMods);
            LoggedIn = true;
            Logs.Log("Logged IN!", "LoaderInit", ConsoleColor.Green);
            //Update.UpdateRPC("Logged in! <3");
        }

        internal static void Load() {
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, args) => Logs.Error((args.ExceptionObject as Exception).ToString());

            HarmonyInstance = new HarmonyLib.Harmony("WorldLoader");
			try {
				Self = new WorldLoader();
				Self.Plugins();
			} catch (Exception e) { 
				Logs.Error(e);
			}
			Menu = new LoaderMenu();
			Internal_Utils.RunInTry(Menu.Show);
			Watermark.Send();
		}

		internal void Awake()
		{
			//Libs.FindLibs();
			_ModManager = new ModManager();
			count = _ModManager.FindMods().Count;
			Logs.Log(ConsoleColor.DarkGray, $"          -==================- {count} Mod{((count == 1) ? String.Empty : "s")} loaded. -==================-");
		}

		internal void Plugins()
		{
			_PluginManager = new PluginManager();
			int count = _PluginManager.LoadPlugins().Count;
			Logs.Log(ConsoleColor.DarkGray, $"          -==================- {count} Plugin{((count == 1) ? "" : "s")} loaded. -==================-");
			foreach (var plugin in _PluginManager.Plugins)
				try {
					plugin.OnLoad();
				}
				catch (Exception e) {
					Logs.Error($"Error OnLoad on Plugin {plugin}", e);
				}
		}

		internal void Start()
		{
			GetReady.Create(new Configuration {
                UnityVersion = InternalInfo.EngineVersion.version
			});

			"==================================- Start -==================================".WriteLineToConsole(ConsoleColor.DarkGray);
			Logs.Log();

			Update.UpdateRPC(null, $"Loading {count} Mods...");

			foreach (UnityMod vrMod in _ModManager.Mods) {
				try {
					"[OnInject] - ".WriteToConsole(ConsoleColor.Green).WriteLineToConsole(vrMod.Name, ConsoleColor.Magenta);
					vrMod.OnInject();
				}
				catch (Exception e) {
                    Logs.Error("Error In OnInject For " + vrMod.Name, e);
				}
			}
			try {
                SceneManagementInit();
                if (!ClassInjector.IsTypeRegisteredInIl2Cpp(typeof(MonoBehv)))
                    ClassInjector.RegisterTypeInIl2Cpp<MonoBehv>();
                var obj = new GameObject("TestOBJ").AddComponent<MonoBehv>();
                UnityEngine.Object.DontDestroyOnLoad(obj);
                MonoBhvMade = true;

            }
			catch (Exception e) {
				Logs.Error("Error On Override", e);
			}

			"==================================- Start -==================================".WriteLineToConsole(ConsoleColor.DarkGray);
			Logs.Log();
			"[Start]".WriteToConsole(ConsoleColor.DarkGreen).WriteLineToConsole("Done Loading Mods!");
			Update.UpdateRPC(null, $"Using {count} Mod{((count == 1) ? String.Empty : "s")}!");
			C.L.Save();
		}
	}
}
