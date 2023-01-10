using System;
using WorldLoader.Mods;
using WorldLoader.HookUtils;
using WorldLoader.Discord;
using WorldLoader.Utils;
using Il2CppInterop;
using WorldLoader.DataClasses;
//using Il2CppInterop.HarmonySupport;
using WorldLoader.ModulesLibs.Managers;
using System.Threading.Tasks;
using Runtime.Il2cpp;

namespace WorldLoader
{
    public partial class WorldLoader
	{
		public static readonly bool IsAuth = LoggedIn;
		private static bool LoggedIn = false;
		internal static bool MonoBhvMade = false;
		private static int count;

		//public static UnityAppInfo appInfo { get; private set; }
		internal static AssemblyResolveManager _AssemblyResolveManager { get; set; }
		internal static LoaderMenu Menu { get; set; }
		//public static HarmonySupportComponent harmonySupportComponent { get; set; }
		internal static HarmonyLib.Harmony HarmonyInstance { get; set; }

		protected internal static void Login() {
            Internal_Utils.RunInTry(Load, null, true);
			Internal_Utils.RunInTry(Preint.Start);
			Menu = new LoaderMenu();
			Internal_Utils.RunInTry(Menu.Show);

			Logs.Log(ConsoleColor.DarkCyan, "Logging In And Checking Tags...");
            Logs.Debug(" -=========================== Debug Mode On! ===========================- ", ConsoleColor.Gray);
			Il2CppUnhollower.Core.OnInitialize();
            Awake();
            Internal_Utils.RunInTry(Start);
            LoggedIn = true;
            Logs.Log("Welcome to WL <3", "LoaderInit", ConsoleColor.Green);
            //Update.UpdateRPC("Logged in! <3");
        }

        internal static void Load() {
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, args) => Logs.Error((args.ExceptionObject as Exception).ToString());
			TaskScheduler.UnobservedTaskException  +=
				(sender, args) => Logs.Error(args.Exception.ToString());

			HarmonyInstance = new HarmonyLib.Harmony("WorldLoader");
            //harmonySupportComponent = new();
            //harmonySupportComponent.Start();

            Watermark.Send();
            //appInfo = new();
        }

		internal static void Awake()
		{
			//Libs.FindLibs();
			count = ModManager.FindMods().Count;
			Logs.Log(ConsoleColor.DarkGray, $"          -==================- {count} Mod{((count == 1) ? String.Empty : "s")} loaded. -==================-");
		}

		internal static void Plugins()
		{
			PluginManager.LoadPlugins();
			int count = PluginManager.LoadedPlugins.Count;
			Logs.Log(ConsoleColor.DarkGray, $"          -==================- {count} Plugin{((count == 1) ? "" : "s")} loaded. -==================-");
			foreach (var plugin in PluginManager.LoadedPlugins)
				try {
					plugin.OnLoad();
				}
				catch (Exception e) {
					Logs.Error($"Error OnLoad on Plugin {plugin}", e);
				}
		}

		internal static void Start()
		{
			InternalInfo.EngineVersion = new UnityVer();
			GetReady.Create(new Configuration {
                UnityVersion = InternalInfo.EngineVersion.version
			});

			try {
				SceneHook.SceneManagementInit();
			}
			catch (Exception e) {
				Logs.Error(e);
			}

			"==================================- Start -==================================".WriteLineToConsole(ConsoleColor.DarkGray);
			Logs.Log();

			Update.UpdateRPC(null, $"Loading {count} Mods...");
            Internal_Utils.SetUpMainMono();

			foreach (UnityMod vrMod in ModManager.Mods.Keys) {
				try {
					"[OnInject] -".WriteToConsole(ConsoleColor.Green).WriteLineToConsole(vrMod.Name, ConsoleColor.Magenta);
					vrMod.OnInject();
				}
				catch (Exception e) {
                    vrMod.Error(e);
				}
			}

			"==================================- Start -==================================".WriteLineToConsole(ConsoleColor.DarkGray);
			Logs.Log();
			"[Start]".WriteToConsole(ConsoleColor.DarkGreen).WriteLineToConsole("Done Loading Mods!", ConsoleColor.DarkGreen);
			Update.UpdateRPC(null, $"Using {count} Mod{((count == 1) ? String.Empty : "s")}!");
			C.L.Save();
		}
	}
}
