﻿using System;

namespace Il2CppGen.Runtime.Runtime;

public interface INativeStructHandler
{
    public int Size();
}

public interface INativeStruct
{
    IntPtr Pointer { get; }
}
