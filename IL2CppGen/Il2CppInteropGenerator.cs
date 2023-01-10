using System.Collections.Generic;
using Il2CppInterop.Internal;
using Il2CppInterop.Internal.Host;
using Il2CppInterop.Internal.XrefScans;
using Il2CppInterop.Generator.Runners;
using Il2CppInterop.Generator.XrefScans;


namespace Il2CppInterop.Generator;

public sealed class Il2CppInteropGenerator : BaseHost
{
    private GeneratorOptions Options;

    private readonly List<IRunner> _runners = new();

    private Il2CppInteropGenerator() { }

    public static Il2CppInteropGenerator Create(GeneratorOptions options)
    {
        var generator = new Il2CppInteropGenerator { Options = options };
        generator.AddXrefScanner<Il2CppInteropGenerator, XrefScanImpl>();
        return generator;
    }

    public override void Start()
    {
        base.Start();

        foreach (var runner in _runners)
        {
            Logger.Instance.LogTrace($"Running {runner.GetType().Name}");
            runner.Run(Options);
        }
    }

    public override void Dispose()
    {
        foreach (var runner in _runners)
            runner.Dispose();
        _runners.Clear();
        base.Dispose();
    }

    public void Run()
    {
        Start();
        Dispose();
    }

    public Il2CppInteropGenerator AddRunner<T>() where T : IRunner, new()
    {
        _runners.Add(new T());
        return this;
    }
}
