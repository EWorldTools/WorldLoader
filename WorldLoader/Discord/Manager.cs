using System;
using System.Runtime.InteropServices;

internal class DiscordRpc
{
    internal struct EventHandlers
    {
        internal DiscordRpc.ReadyCallback readyCallback;
        internal DiscordRpc.DisconnectedCallback disconnectedCallback;
        internal DiscordRpc.ErrorCallback errorCallback;
        internal DiscordRpc.JoinCallback joinCallback;
        internal DiscordRpc.SpectateCallback spectateCallback;
    }

    [Serializable]
    internal struct RichPresence
    {
        internal string state;
        internal string details;
        internal long startTimestamp;
        internal long endTimestamp;
        internal string largeImageKey;
        internal string largeImageText;
        internal string smallImageKey;
        internal string smallImageText;
        internal string partyId;
        internal int partySize;
        internal int partyMax;
        internal string matchSecret;
        internal string joinSecret;
        internal string spectateSecret;
        internal bool instance;
        internal string buttons;

    }


    public enum Reply
    {
        No,
        Yes,
        Ignore
    }

    [DllImport("WorldLoader/Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Initialize")]
    internal protected static extern void Initialize(string applicationId, ref DiscordRpc.EventHandlers handlers, bool autoRegister, string optionalSteamId);

    [DllImport("WorldLoader/Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Shutdown")]
    internal protected static extern void Shutdown();

    [DllImport("WorldLoader/Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RunCallbacks")]
    internal protected static extern void RunCallbacks();

    [DllImport("WorldLoader/Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UpdatePresence")]
    internal protected static extern void UpdatePresence(ref DiscordRpc.RichPresence presence);

    [DllImport("WorldLoader/Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClearPresence")]
    internal protected static extern void ClearPresence();

    [DllImport("WorldLoader/Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Respond")]
    internal protected static extern void Respond(string userId, DiscordRpc.Reply reply);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal protected delegate void ReadyCallback();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal protected delegate void DisconnectedCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal protected delegate void ErrorCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal protected delegate void JoinCallback(string secret);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal protected delegate void SpectateCallback(string secret);
}