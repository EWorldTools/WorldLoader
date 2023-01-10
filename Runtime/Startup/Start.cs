using System;
using Il2CppInterop.Internal.Host;
using Il2CppInterop.Internal.XrefScans;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.Runtime;
using Il2CppInterop.Runtime.XrefScans;

namespace Il2CppInterop;

public record Configuration
{
    public Version UnityVersion { get; set; }
}

public sealed class GetReady : BaseHost
{
    private GetReady()
    {
    }

    public static GetReady Instance => GetInstance<GetReady>();

    public Version UnityVersion {get; private set; }


    public static GetReady Create(Configuration configuration)
    {
        var res = new GetReady();
        res.UnityVersion = configuration.UnityVersion;

        SetInstance(res);
        res.AddXrefScanner<GetReady, XrefScanImpl>();
        UnityVersionHandler.RecalculateHandlers();
        return res;
    }

    public override void Start()
    {
        UnityVersionHandler.RecalculateHandlers();
        base.Start();
    }
}
