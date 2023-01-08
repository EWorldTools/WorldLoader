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

		internal void Initialize(PluginAttribute PluginInfo)
		{
			this.Name = PluginInfo.Name;
			this.Version = PluginInfo.Version.ToString(); // TODO: THIS
			this.Author = PluginInfo.Author;
		}
	
		/// <summary>
		///  Runs OnInject
		/// </summary>
		public virtual void OnLoad()
		{
		}

	}
}
