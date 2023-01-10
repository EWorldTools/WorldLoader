using System;

namespace Il2CppInterop.Runtime.Runtime.VersionSpecific.Image;

public interface INativeImageStructHandler : INativeStructHandler
{
    INativeImageStruct CreateNewStruct();
    unsafe INativeImageStruct Wrap(Il2CppImage* imagePointer);
}

public interface INativeImageStruct : INativeStruct
{
    unsafe Il2CppImage* ImagePointer { get; }

    unsafe ref Il2CppAssembly* Assembly { get; }

    ref byte Dynamic { get; }

    ref IntPtr Name { get; }

    bool HasNameNoExt { get; }

    ref IntPtr NameNoExt { get; }
}
