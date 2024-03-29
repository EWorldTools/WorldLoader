﻿using System;
using System.IO;

namespace WorldLoader.HookUtils;
public static class Logs
{
	internal static bool IsWriting;
	internal static bool AddTime = false;

	public static string LogLocation { get; private set; } = "WorldLoader\\Log.txt";

	// New Stuff (i like these a lot ^w^

	public static string WriteToConsole(this string text, ConsoleColor color, string[] flags = null, ConsoleColor flagColor = ConsoleColor.DarkRed)
	{
		string flagsToLog = "";
		var time = WriteTime();
		Console.ForegroundColor = flagColor;
		Console.Write("[");
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.Write("WorldLoader");
		Console.ForegroundColor = flagColor;
		Console.Write("] ");
		if (flags != null && flags.Length != 0) foreach (var flag in flags) flagsToLog += $"[{flag}] ";
		Console.Write(flagsToLog);
		Console.ForegroundColor = color;
		Console.Write(text + " ");
		Console.ResetColor();
		return text;
	}

	//public static string WriteToConsole(this string text, ConsoleColor color, string flag = null, ConsoleColor flagColor = ConsoleColor.DarkRed) =>
	//	text.WriteToConsole(color, new string[] { 
	//		flag
	//	}, flagColor);


	public static string WriteToConsole(this string text, string AddText, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.Write(text.EndsWith(" ") ? String.Empty : " " + AddText);
		Console.ResetColor();
		return text;
	}

	public static string WriteToConsole(this string text, ConsoleColor color, string AddText)
	{
		Console.ForegroundColor = color;
		Console.Write(text.EndsWith(" ") ? String.Empty : " " + AddText);
		Console.ResetColor();
		return text;
	}

	public static void WriteLineToConsole(this string text, string Write, ConsoleColor color = ConsoleColor.DarkCyan)
	{
		Console.ForegroundColor = color;
		Console.WriteLine(text.EndsWith(" ") ? String.Empty : " " + Write);
		Console.ResetColor();
	}

	public static string WriteLineToConsole(this string text, ConsoleColor color, string[] flags = null, ConsoleColor flagColor = ConsoleColor.DarkRed)
	{
		string flagsToLog = "";
		var time = WriteTime();
		Console.ForegroundColor = flagColor;
		Console.Write("[");
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.Write("WorldLoader");
		Console.ForegroundColor = flagColor;
		Console.Write("] ");
		if (flags != null && flags.Length != 0) foreach (var flag in flags) flagsToLog += $"[{flag}] ";
		Console.Write(flagsToLog);
		Console.ForegroundColor = color;
		Console.WriteLine(text);
		Console.ResetColor();
		return text;
	}

	public static string WriteLineToConsole(this string text, string[] flags = null, ConsoleColor flagColor = ConsoleColor.DarkRed)
	{
		string flagsToLog = "";
		var time = WriteTime();
		Console.ForegroundColor = flagColor;
		Console.Write("[");
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.Write("WorldLoader");
		Console.ForegroundColor = flagColor;
		Console.Write("] ");
		if (flags != null && flags.Length != 0) foreach (var flag in flags) flagsToLog += $"[{flag}] ";
		Console.Write(flagsToLog);
		Console.ForegroundColor = GetRandomConsoleColor();
		Console.WriteLine(text);
		Console.ResetColor();
		return text;
	}

	public static string WriteLineToConsole(this string text, ConsoleColor color, string flag, ConsoleColor flagColor = ConsoleColor.DarkRed) =>
		text.WriteLineToConsole(color, new string[] {
				flag
		}, flagColor);


	// New Stuff (i like these a lot ^w^

	public static void Log(string message = null, string flag = "", ConsoleColor Color = ConsoleColor.White, ConsoleColor flagColor = ConsoleColor.DarkMagenta, string Name = "WorldLoader")
	{
		if (message == null) {
			Console.WriteLine();
			return;
		}
		var time = WriteTime();
		Console.ForegroundColor = flagColor;
		Console.Write("[");
		Console.Write(Name);
		Console.Write("] " + (string.IsNullOrEmpty(flag) ? "" : $"[{flag}] "));
		Console.ForegroundColor = Color;
		Console.WriteLine(message);
		Console.ForegroundColor = ConsoleColor.Gray;
	}

	// Just Here to make things Faster
	public static void Log(ConsoleColor Color, string message, string flag = "") =>
		Log(message, flag, Color);

	// Just Here to make things Faster
	public static void Log(string message, string flag, string flag2, ConsoleColor Color = ConsoleColor.White, ConsoleColor flagColor = ConsoleColor.Red, string Name = "WorldLoader") =>
		Log(message, $"{flag}] [" + flag2, Color);

	public static void Debug(string message, string flag, string Name = "WorldLoader", ConsoleColor Color = ConsoleColor.Gray) =>
		Debug(message, Color, Name, flag);

	public static void Warn(string message = null, string flag = "", ConsoleColor Color = ConsoleColor.White, ConsoleColor flagColor = ConsoleColor.Red, string Name = "WorldLoader") {
		if (message == null) {
			Console.WriteLine();
			return;
		}
		var time = WriteTime();
		Console.ForegroundColor = flagColor;
		Console.Write("[");
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.Write(Name);
		Console.ForegroundColor = flagColor;
		Console.Write("] [Warn] " + (string.IsNullOrEmpty(flag) ? "" : $"[{flag}] "));
		Console.ForegroundColor = Color;
		Console.WriteLine(message);
		Console.ForegroundColor = ConsoleColor.Gray;
	}

	public static string Debug(string message = null, ConsoleColor Color = ConsoleColor.White, string Name = "WorldLoader", string flag = "") {
		if (C.L.Config.Debug) {
			if (message == null) {
				Console.WriteLine();
				return null;
			}
			WriteTime();
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("[");
			Console.Write(Name);
			Console.Write("] [Debug] " + (string.IsNullOrWhiteSpace(flag) ? "" : $"[{flag}] "));
			Console.ForegroundColor = Color;
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		return $"{DateTime.Now.ToString("HH:mm:ss.fff")}[{Name}] [Debug] " + message;
	}

	public static void Error(Exception e) => Error(null, e);

	public static void Error(string message, Exception Error = null, string Name = "WorldLoader")
	{
		var time = WriteTime();
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write("[");
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write(Name);
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write("] ");
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(string.IsNullOrEmpty(message) ? "Unknown Error: " : message + "\n");
		Console.ForegroundColor = ConsoleColor.Gray;
		if (Error != null) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"{Error}");

			Console.ForegroundColor = ConsoleColor.Gray;
		}
	}

	internal static string WriteTime(ConsoleColor color = ConsoleColor.DarkRed, ConsoleColor color2 = ConsoleColor.DarkRed) {
		if (!AddTime) return "";
		Console.ForegroundColor = color2;
		Console.Write("[");
		Console.ForegroundColor = color;
		string SystemTime = DateTime.Now.ToString("HH:mm:ss.fff");
		Console.Write(SystemTime);
		Console.ForegroundColor = color2;
		Console.Write("] ");
		Console.ResetColor();
		return $"[{SystemTime}] ";
	}

	private static ConsoleColor lastcolor;

	public static ConsoleColor GetRandomConsoleColor() {
		var ca = new Random().Next(1, 16);
		var color = (ConsoleColor)ca;
		if (color == lastcolor)
			color = (ConsoleColor)Enum.GetValues(typeof(ConsoleColor)).GetValue(ca - 1);
		if (color == ConsoleColor.Black || color == ConsoleColor.Red)
			return ConsoleColor.DarkMagenta;
		lastcolor = color;
		return color;
	}
}