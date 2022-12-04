using System;
using System.Reflection;
using System.Runtime.InteropServices;
using WorldLoader.HookUtils;
using WorldLoader.Utils;

namespace WorldLoader.Init;

[ComImport, Guid("cfe72319-45e3-4e7f-9635-c533c5d66ddf"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface INetDomain {
	void OnCLRInit();
	void MinHook_CreateInstance(IntPtr CreateHook, IntPtr RemoveHook, IntPtr EnableHook, IntPtr DisableHook);
}


[Obfuscation(Exclude = true, ApplyToMembers = false)]
internal sealed class Interface : AppDomainManager, INetDomain
{
	[Obfuscation(Exclude = true, ApplyToMembers = false)]
	public override void InitializeNewDomain(AppDomainSetup appDomainInfo) =>
		InitializationFlags = AppDomainManagerInitializationOptions.RegisterWithHost;

	[Obfuscation(Exclude = true, ApplyToMembers = false)]
	public void OnCLRInit() => 
		Internal_Utils.RunInTry(WorldLoader.Login);

	[Obfuscation(Exclude = true, ApplyToMembers = false)]
	public static void StartLoadMods() =>
		global::WorldLoader.WorldLoader.Self.Start();

	[Obfuscation(Exclude = true, ApplyToMembers = false)]
	public void MinHook_CreateInstance(IntPtr CreateHook, IntPtr RemoveHook, IntPtr EnableHook, IntPtr DisableHook) =>
		Internal_Utils.MinHookCreateInstance(CreateHook, RemoveHook, EnableHook, DisableHook);
}
