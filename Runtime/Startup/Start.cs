using System;
using WorldLoader.Il2CppGen.Internal.Host;
using WorldLoader.Il2CppGen.Internal.XrefScans;
using Il2CppGen.Runtime.Injection;
using Il2CppGen.Runtime.Runtime;
using Il2CppGen.Runtime.XrefScans;

namespace Il2CppGen;

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
