using System;

namespace Il2CppInterop.Generator.Runners;

public interface IRunner : IDisposable
{
    void Run(GeneratorOptions options);
}
