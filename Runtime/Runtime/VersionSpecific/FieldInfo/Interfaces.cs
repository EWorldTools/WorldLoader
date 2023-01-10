﻿using System;

namespace Il2CppInterop.Runtime.Runtime.VersionSpecific.FieldInfo;

public interface INativeFieldInfoStructHandler : INativeStructHandler
{
    INativeFieldInfoStruct CreateNewStruct();
    unsafe INativeFieldInfoStruct Wrap(Il2CppFieldInfo* fieldInfoPointer);
}

public interface INativeFieldInfoStruct : INativeStruct
{
    unsafe Il2CppFieldInfo* FieldInfoPointer { get; }

    ref IntPtr Name { get; }

    unsafe ref Il2CppTypeStruct* Type { get; }

    unsafe ref Il2CppClass* Parent { get; }

    ref int Offset { get; }
}
