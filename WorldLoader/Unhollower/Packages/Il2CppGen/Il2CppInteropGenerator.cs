using System.Collections.Generic;
using WorldLoader.Il2CppGen.Internal;
using WorldLoader.Il2CppGen.Internal.Host;
using WorldLoader.Il2CppGen.Internal.XrefScans;
using WorldLoader.Il2CppGen.Generator.Runners;
using WorldLoader.Il2CppGen.Generator.XrefScans;


namespace WorldLoader.Il2CppGen.Generator;

public sealed class Il2CppGenGenerator : BaseHost
{
    private GeneratorOptions Options;

    private readonly List<IRunner> _runners = new();

    private Il2CppGenGenerator() { }

    internal static Il2CppGenGenerator Create(GeneratorOptions options)
    {
        var generator = new Il2CppGenGenerator { Options = options };
        generator.AddXrefScanner<Il2CppGenGenerator, XrefScanImpl>();
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

    internal void Run()
    {
        Start();
        Dispose();
    }

    internal Il2CppGenGenerator AddRunner<T>() where T : IRunner, new()
    {
        _runners.Add(new T());
        return this;
    }
}
