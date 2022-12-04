using System;

namespace WorldLoader.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ModAttribute : Attribute
{
	public string Name { get; }
	public string Version { get; }
	public string Author { get; }
	public string Link { get; }
	public ConsoleColor ModColor { get; }
	public ModAttribute(string name, string version, string author)
	{
		this.Name = name;
		this.Version = version;
		this.Author = author;

	}
}
/// <summary>
///  These Are Loaded Right After Auth (Before Mods)
/// </summary>
/// <param name="name"></param>
/// <param name="version"></param>
/// <param name="author"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PluginAttribute : Attribute
{
	public string Name { get; }
	public int Version { get; }
	public string Author { get; }
	public string Link { get; }
	public ConsoleColor ModColor { get; }
	/// <summary>
	///  These Are Loaded Right After Auth (Before Mods)
	/// </summary>
	/// <param name="name"></param>
	/// <param name="version"></param>
	/// <param name="author"></param>
	public PluginAttribute(string name, int version, string author)
	{
		this.Name = name;
		this.Version = version;
		this.Author = author;

	}
} 
