using System;

namespace WorldLoader.Il2CppGen.Generator.Runners;

internal interface IRunner : IDisposable
{
    void Run(GeneratorOptions options);
}
