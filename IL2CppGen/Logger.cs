using System;
using Il2CppInterop.Internal.Host;
using WorldLoader.HookUtils;

namespace Il2CppInterop.Internal;

public class Logger
{
    public class Instance {


        public static void LogTrace(string Message) {
            if (C.L.Config.UnhollowerLogTrace) Logs.Debug(Message, "Assembly Generation] [Trace");
        }

        public static void LogWarning(string Message) => Logs.Warn("[Warning] " + Message, "Assembly Generation");
        public static void LogError(string Message) => Logs.Error("[Error] " + Message);
        public static void LogInformation(string Message) => Logs.Log("[Info] " + Message, "Assembly Generation");
    }

    public Logger()
    {
    }

    public void Start()
    {
    }

    public void Dispose()
    {
    }
}
