using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.HookUtils;

namespace WorldLoader.ModulesLibs.UserInterface;

public class WLMod
{
	public Type type { get; internal set; }

	public Harmony harmonyInstance { get; internal set; }
	public string Name { get; set; }
	public string Version { get; set; }
	public string Author { get; set; }
	public string Link { get; set; }
	public ConsoleColor ModColor { get; set; } = ConsoleColor.Magenta;


	/// <summary>
	///  Log 
	/// </summary>
	/// <param name="message"></param>
	/// <param name="color"></param>
	public void Log(string message = null, ConsoleColor color = ConsoleColor.DarkGray)
	{
		if (message == null) {
			Console.WriteLine();
			return;
		}
		Logs.Log(message, null, color, this.ModColor, this.Name);
	}

	/// <summary>
	///  Log 
	/// </summary>
	/// <param name="message"></param>
	/// <param name="color"></param>
	public void Log(ConsoleColor color = ConsoleColor.DarkGray, string message = null)
	{
		if (String.IsNullOrWhiteSpace(message)) {
			Console.WriteLine();
			return;
		}
		Logs.Log(message, null, color, this.ModColor, this.Name);
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
		if (message == null && e == null) {
			Console.WriteLine();
			return;
		}
		Logs.Error(String.IsNullOrWhiteSpace(message) ? "[Error] " : message, e, this.Name);
	}

	public override string ToString() =>
		$"{Name} [{Version}] by - {Author}";


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