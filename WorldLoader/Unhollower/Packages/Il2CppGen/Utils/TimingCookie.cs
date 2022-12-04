using System;
using System.Diagnostics;
using WorldLoader.Il2CppGen.Internal;


namespace WorldLoader.Il2CppGen.Generator.Utils;

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
