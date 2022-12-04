using System;
using System.Collections.Generic;
using WorldLoader.Attributes;
using WorldLoader.HookUtils;

namespace WorldLoader.Plugins
{
	public class WLPlugin
	{
		public WLPlugin() {
		}

		internal WLPlugin(Type type) {
			this.type = type;
		}

		public Type type { get; private set; }


		public string Name { get; private set; }
		public int Version { get; private set; }
		public string Author { get; private set; }
		public string Link { get; private set; }
		public ConsoleColor PluginColor { get; private set; } = ConsoleColor.Magenta;
		public PluginManager PluginManager { get; private set; }

		public virtual void Unload() =>
			PluginManager.UnloadPlugin(this);
		

		internal void Initialize(PluginAttribute PluginInfo, PluginManager PluginMger)
		{
			this.Name = PluginInfo.Name;
			this.Version = PluginInfo.Version;
			this.Author = PluginInfo.Author;
			this.PluginManager = PluginMger;
		}

		public override string ToString() =>
			 string.Format("{0} ({1}) by {2}", Name, Version, Author);

		/// <summary>
		///  Log (Uses Plugin Name, ConsoleColor.Black = Plugin Color)
		/// </summary>
		/// <param name="message"></param>
		/// <param name="color"></param>
		public void Log(string message = null, ConsoleColor color = ConsoleColor.Black)
		{
			if (message == null) {
				Console.WriteLine();
				return;
			}
			Logs.Log(message, null, color.Equals(ConsoleColor.Black) ? this.PluginColor : color, ConsoleColor.Red, this.Name);
		}

		/// <summary>
		///  Log (Uses Plugin Name, ConsoleColor.Black = Plugin Color)
		/// </summary>
		/// <param name="message"></param>
		/// <param name="color"></param>
		public void Log(ConsoleColor color = ConsoleColor.Black, string message = null)
		{
			if (String.IsNullOrWhiteSpace(message)) {
				Console.WriteLine();
				return;
			}
			Logs.Log(message, null, color.Equals(ConsoleColor.Black) ? this.PluginColor : color, ConsoleColor.Red, this.Name);
		}

		public void Error(string message = null, Exception e = null)
		{
			if (message == null && e == null) {
				Console.WriteLine();
				return;
			}
			Logs.Error(String.IsNullOrWhiteSpace(message) ? "[Error] " : message, e, this.Name);
		}

		public void Error(Exception e = null, string message = null)
		{
			if (message == null && e == null)
			{
				Console.WriteLine();
				return;
			}
			Logs.Error(String.IsNullOrWhiteSpace(message) ? "[Error] " : message, e, this.Name);
		}

		/// <summary>
		///  Runs OnInject
		/// </summary>
		public virtual void OnLoad()
		{
		}

		/// <summary>
		///  Runs EveryFrame
		/// </summary>
		public virtual void OnUpdate()
		{
		}

		/// <summary>
		///  Loads When a Scene Is Loaded 
		/// </summary>
		/// <param name="buildIndex"></param>
		/// <param name="sceneName"></param>
		public virtual void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
		}

		/// <summary>
		///  Loads When a Scene is Unloaded
		/// </summary>
		/// <param name="buildIndex"></param>
		/// <param name="sceneName"></param>
		public virtual void OnSceneWasUnloaded(int buildIndex, string sceneName)
		{
		}

	}
}
