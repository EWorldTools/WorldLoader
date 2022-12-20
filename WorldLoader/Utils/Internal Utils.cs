using Il2CppGen.Runtime;
using Il2CppGen.Runtime.Injection;
using Il2CppGen.Runtime.Runtime;
using Il2CppGen.Runtime.Runtime.VersionSpecific.Class;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldLoader.DataClasses;
using WorldLoader.HookUtils;

namespace WorldLoader.Utils;

internal static class Internal_Utils {
	internal static Dictionary<string, string> UnhollowedAssemblys { get; set; } = new();
	public static List<DirectoryInfo> AdditionalChecks = new();
	[DllImport("kernel32.dll")]
	private static extern int AllocConsole();

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool SetForegroundWindow(IntPtr hWnd);

	[DllImport("kernel32.dll")]
	private static extern IntPtr GetConsoleWindow();

	internal static IntPtr ReadClassPointerForType(Type type)
	{
		return (IntPtr)typeof(Il2CppClassPointerStore<>).MakeGenericType(type)
			.GetField(nameof(Il2CppClassPointerStore<int>.NativeClassPtr)).GetValue(null);
	}

	internal static unsafe void SetUpMainMono()
    {
		var rev = new RegisterTypeOptions();
		var it = typeof(Il2CppSystem.Collections.IEnumerator);
		var classPointer = ReadClassPointerForType(it);
		if (classPointer == IntPtr.Zero)
			throw new ArgumentException($"Type {it} doesn't have an IL2CPP class pointer, which means it's not an IL2CPP interface");

		var prt = UnityVersionHandler.Wrap((Il2CppClass*)classPointer);
		List<INativeClassStruct> structs = new List<INativeClassStruct>();
		structs.Add(prt);
		rev.Interfaces = new Il2CppInterfaceCollection(structs);
			
		if (Il2CppClassPointerStore<MonoEnumeratorWrapper>.NativeClassPtr == IntPtr.Zero)
			ClassInjector.RegisterTypeInIl2Cpp<MonoEnumeratorWrapper>(rev);
		new Runtime.Il2cpp.Main();
	}

	internal static void PrepConsole() {
		AllocConsole();
		var Swr = new StreamWriter(Console.OpenStandardOutput()) {
			AutoFlush = true
		};
		Console.SetOut(Swr);
		Console.SetIn(new StreamReader(Console.OpenStandardInput()));
		Console.Clear();
		Console.Title = "WorldLoader";
		SetForegroundWindow(GetConsoleWindow());
		Console.CursorVisible = false;
		Console.OutputEncoding = Encoding.UTF8;
	}

	internal static void RunInTry(this Action action, string ErrorMessage = null, bool ShowError = false) { 
		try {
			action();
		} catch (Exception e) {
			Logs.Error(ErrorMessage, e);
			if (ShowError) MessageBox.Show(e.ToString(), "Fatal Error");
		}
	}


	public static string RemoveFullPath(this string fileOrDirectoryName)
	{ // i used AI to gen this o.o BE SCAREDD
	  // Get the index of the last directory separator character
		int lastSeparatorIndex = fileOrDirectoryName.LastIndexOf(Path.DirectorySeparatorChar);

		// If a separator was found, return the file or directory name without the Path
		if (lastSeparatorIndex >= 0)
			return fileOrDirectoryName.Substring(lastSeparatorIndex + 1);
		// Otherwise, return the original file or directory name
		else
			return fileOrDirectoryName;
	}


	// Because, for no reason it had to be so mega extra (at AssemblyResolveFix (Line 95, InternalUtils) It kept on giving an error cause it was trying to acess the Unity Core, FOR NO REASON
	// IT DOESN'T NEED THE UNITY CORE HERE
	// https://media.discordapp.net/attachments/983750352476176494/1054574598773018795/image.png
	internal static void AssemblyResolveFix() {
		var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\WorldLoader\\UnhollowedAsm");
		if (files == null) throw new Exception("Files Are Null!");
		foreach (var Asm in files) {
			if (Asm.EndsWith(".dll"))
				UnhollowedAssemblys.Add(Asm.RemoveFullPath(), Asm);
		}
		AdditionalChecks.Add(Directory.CreateDirectory("UserLibs"));
		AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
			string name = args.Name;
			byte[] ByteData = null;
			if (args.Name.Contains(','))
				name = args.Name.Substring(0, args.Name.IndexOf(','));// This isn't too good, but it works ig
			Logs.Debug($"[AssemblyResolve] Failed Finding an Assembly Normally! Checking Unhollowed For Assembly {name} ({sender})");
			if (UnhollowedAssemblys.TryGetValue(name + ".dll", out var asm))
				return Assembly.Load(File.ReadAllBytes(asm));
			if (AdditionalChecks.Count > 0) AdditionalChecks.ForEach(a => {
				var files = a.GetFiles();
				var filepath = files.FirstOrDefault(a => a.Name == name).FullName;
				if (!string.IsNullOrEmpty(filepath))
				ByteData = File.ReadAllBytes(filepath);
			});
			if (ByteData.Length > 10)
				return Assembly.Load(ByteData);
			Logs.Warn($"Sender {sender} Tried to get an Assembly ({name}) and FAILED!");
			return null;
		};
	}

	internal static void RunInTry(this MethodInfo Info, string ErrorMessage = null) => RunInTry(Info, ErrorMessage);


	internal static void MinHookCreateInstance(IntPtr CreateHook, IntPtr RemoveHook, IntPtr EnableHook, IntPtr DisableHook) {
		Internal_Utils.RunInTry(Internal_Utils.PrepConsole);
		MinHook.HookInprts.EnableHook = EnableHook;
		MinHook.HookInprts.CreateHook = CreateHook;
		MinHook.HookInprts.RemoveHook = RemoveHook;
		MinHook.HookInprts.DisableHook = DisableHook;
		MinHook.CreateHook = NativeUtils.CreateDelegate<MinHook._CreateHook>(CreateHook);
		MinHook.RemoveHook = NativeUtils.CreateDelegate<MinHook._RemoveHook>(RemoveHook);
		MinHook.EnableHook = NativeUtils.CreateDelegate<MinHook._EnableHook>(EnableHook);
		MinHook.DisableHook = NativeUtils.CreateDelegate<MinHook._DisableHook>(DisableHook);
	}

	internal static void KillEAC() {
		foreach (var runningPr in Process.GetProcesses())
			if (runningPr.ProcessName == "EasyAntiCheat_EOS")
				runningPr.Kill();
	}
}

