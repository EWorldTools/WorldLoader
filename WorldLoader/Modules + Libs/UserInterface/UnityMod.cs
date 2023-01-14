using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using WorldLoader.Attributes;
using WorldLoader.HookUtils;
using WorldLoader.ModulesLibs.Managers;
using WorldLoader.ModulesLibs.UserInterface;
using WorldLoader.Utils;

namespace WorldLoader.Mods
{
	public class UnityMod : WLMod
	{
		public UnityMod() {
		}

		public UnityMod(string name, ConsoleColor color = ConsoleColor.DarkRed) {
			Name = name;
			ModColor = color;
		}

		internal UnityMod(Type type) {
			this.type = type;
		}
		public UnityMod Mod { get; private set; }
		public bool AllowUnloading { get; set; } = true;


		public virtual void OnUnload() { 
			
		}

		public void Unload() {
			OnUnload();
			ModManager.UnloadMod(this);
			harmonyInstance.UnpatchSelf();
		}


		internal void Initialize(ModAttribute ModInfo)
		{
			Name = ModInfo.Name;
			Version = ModInfo.Version;
			Author = ModInfo.Author;
			Mod = this;
			harmonyInstance = new(Name + Guid.NewGuid()); // Yeah Unix, im pretty stupud ~w~
		}

		/// <summary>
		///  Runs OnInject
		/// </summary>
		public virtual void OnInject()
		{
		}

		/// <summary>
		///  Runs a few times EveryFrame
		/// </summary>
		public virtual void OnGui()
		{
		}

	}
}
