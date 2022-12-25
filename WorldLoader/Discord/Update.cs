using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.HookUtils;

namespace WorldLoader.Discord;

internal class Update
{
    public static void UpdateRPC(string state = null, string details = null, int partySize = -1)
    {
        if (!Discord.Active) 
            return;
        Logs.Debug($"Updated Presence At {DateTime.Now}:{DateTime.Now.Second} With Value "
            + (string.IsNullOrEmpty(state) ? string.Empty : $"State {state} ")
            + (string.IsNullOrEmpty(details) ? string.Empty : $"Details {details}"));

        var presence = Discord.presence;
        if (!string.IsNullOrEmpty(state))
            presence.state = state;
        if (!string.IsNullOrEmpty(details))
            presence.details = details;
        if (partySize >= 0)
            presence.partySize = partySize;
        DiscordRpc.UpdatePresence(ref presence);
    }
}
