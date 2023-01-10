using System;
using System.Diagnostics;
using Il2CppInterop.Internal;


namespace Il2CppInterop.Generator.Utils;

internal readonly struct TimingCookie : IDisposable
{
    private readonly Stopwatch myStopwatch;

    public TimingCookie(string message)
    {
        Logger.Instance.LogInformation($"{message}...");
        myStopwatch = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        Logger.Instance.LogInformation($"Done in {myStopwatch.Elapsed}");
    }
}
