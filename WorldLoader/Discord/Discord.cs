using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WorldLoader.HookUtils;

namespace WorldLoader.Discord;

public class Discord
{
    internal static DiscordRpc.RichPresence presence;
    internal static DiscordRpc.EventHandlers eventHandlers;

    private static bool privatestate = true;

    public static bool Active { get => privatestate; set {
            privatestate = value;
        }
    }

    internal static void Init()
    {
        eventHandlers = default;
        eventHandlers.errorCallback = delegate (int code, string message) { };
        presence.state = $"Using beta <3";
        presence.largeImageKey = "wclogo";
        presence.largeImageText = $"discord.gg/erpers - <3";
        presence.smallImageKey = "eac"; // Cat Image
        presence.smallImageText = "Imagine <3";
        presence.partySize = 0;
        presence.partyMax = 0;
        presence.startTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        presence.partyId = Guid.NewGuid().ToString();
        try
        {
            DiscordRpc.Initialize("1031605732522590269", ref eventHandlers, true, "");
            DiscordRpc.UpdatePresence(ref presence);
        }
        catch { }
    }
}
