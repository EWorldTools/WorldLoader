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
using WorldLoader.HookUtils;

namespace WorldLoader.Utils;

internal static class Internal_Utils {

	[DllImport("kernel32.dll")]
	private static extern int AllocConsole();

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool SetForegroundWindow(IntPtr hWnd);

	[DllImport("kernel32.dll")]
	private static extern IntPtr GetConsoleWindow();

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

	internal static void RunInTry(this MethodInfo Info, string ErrorMessage = null) => RunInTry(Info, ErrorMessage);


	internal static void MinHookCreateInstance(IntPtr CreateHook, IntPtr RemoveHook, IntPtr EnableHook, IntPtr DisableHook) {
		Internal_Utils.RunInTry(Internal_Utils.PrepConsole);
		MinHook.HookInprts.mVRC_EnableHook = EnableHook;
		MinHook.HookInprts.mVRC_CreateHook = CreateHook;
		MinHook.HookInprts.mVRC_RemoveHook = RemoveHook;
		MinHook.HookInprts.mVRC_DisableHook = DisableHook;
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

