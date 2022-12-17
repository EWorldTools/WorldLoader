using System;
using System.Collections.Generic;
using WorldLoader.Attributes;
using WorldLoader.HookUtils;

namespace WorldLoader.Mods
{
	public class UnityMod
	{
		public UnityMod() {
		}

		public UnityMod(string name, ConsoleColor color = ConsoleColor.DarkRed)
		{
			Name = name;
			ModColor = color;
		}

		internal UnityMod(Type type) {
			this.type = type;
		}

		public Type type { get; private set; }


		public string Name { get; set; }
		public string Version { get; set; }
		public string Author { get; set; }
		public string Link { get; set; }
		public ConsoleColor ModColor { get; set; } = ConsoleColor.Magenta;
		public ModManager ModManager { get; private set; }
		public UnityMod Mod;

		public virtual void OnUnload() { 
			
		}

		public void Unload() {
			OnUnload();
			ModManager.UnloadMod(this);
		}


		internal void Initialize(ModAttribute ModInfo, ModManager ModMger)
		{
			Name = ModInfo.Name;
			Version = ModInfo.Version;
			Author = ModInfo.Author;
			ModManager = ModMger;
			Mod = this;
		}

		public override string ToString() =>
			 string.Format("{0} ({1}) by {2}", Name, Version, Author);

		/// <summary>
		///  Log (Uses Mod Name, ConsoleColor.Black = Mod Color)
		/// </summary>
		/// <param name="message"></param>
		/// <param name="color"></param>
		public void Log(string message = null, ConsoleColor color = ConsoleColor.Black)
		{
			if (message == null) {
				Console.WriteLine();
				return;
			}
			Logs.Log(message, null, color.Equals(ConsoleColor.Black) ? this.ModColor : color, ConsoleColor.Red, this.Name);
		}

		/// <summary>
		///  Log (Uses Mod Name, ConsoleColor.Black = Mod Color)
		/// </summary>
		/// <param name="message"></param>
		/// <param name="color"></param>
		public void Log(ConsoleColor color = ConsoleColor.Black, string message = null)
		{
			if (String.IsNullOrWhiteSpace(message)) {
				Console.WriteLine();
				return;
			}
			Logs.Log(message, null, color.Equals(ConsoleColor.Black) ? this.ModColor : color, ConsoleColor.Red, this.Name);
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
		public virtual void OnInject()
		{
		}

		/// <summary>
		///  Runs EveryFrame
		/// </summary>
		public virtual void OnUpdate()
		{
		}

		/// <summary>
		///  Runs a few times EveryFrame
		/// </summary>
		public virtual void OnGui()
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
