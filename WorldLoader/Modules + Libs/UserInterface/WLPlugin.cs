using System;
using System.Collections.Generic;
using WorldLoader.Attributes;
using WorldLoader.HookUtils;
using WorldLoader.ModulesLibs.Managers;
using WorldLoader.ModulesLibs.UserInterface;

namespace WorldLoader.Plugins
{
	public class WLPlugin : WLMod
	{
		public WLPlugin() {
		}

		internal WLPlugin(Type type) {
			this.type = type;
		}

		public PluginManager PluginManager { get; private set; }

		internal void Initialize(PluginAttribute PluginInfo, PluginManager PluginMger)
		{
			this.Name = PluginInfo.Name;
			this.Version = PluginInfo.Version.ToString(); // TODO: THIS
			this.Author = PluginInfo.Author;
			this.PluginManager = PluginMger;
		}
	
		/// <summary>
		///  Runs OnInject
		/// </summary>
		public virtual void OnLoad()
		{
		}

	}
}
