using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WorldLoader.HookUtils;
using WorldLoader.Utils;

namespace WorldLoader.Init;

[ComImport, Guid("cfe72319-45e3-4e7f-9635-c533c5d66ddf"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface INetDomain {
	void OnCLRInit();
	void MinHook_CreateInstance(IntPtr CreateHook, IntPtr RemoveHook, IntPtr EnableHook, IntPtr DisableHook);
}


internal sealed class Interface : AppDomainManager, INetDomain
{
	public override void InitializeNewDomain(AppDomainSetup appDomainInfo) =>
		InitializationFlags = AppDomainManagerInitializationOptions.RegisterWithHost;

	public void OnCLRInit() => 
		WorldLoader.Login();

	public static void StartLoadMods() =>
		global::WorldLoader.WorldLoader.Self.Start();

	public void MinHook_CreateInstance(IntPtr CreateHook, IntPtr RemoveHook, IntPtr EnableHook, IntPtr DisableHook) {
		try {
			Internal_Utils.MinHookCreateInstance(CreateHook, RemoveHook, EnableHook, DisableHook);
		}
		catch (Exception e) { MessageBox.Show(e.ToString(), "Failt ERROR"); }
	}
}
