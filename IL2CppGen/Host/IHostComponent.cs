using System;

namespace Il2CppInterop.Internal.Host;

public interface IHostComponent : IDisposable
{
    void Start();
}
