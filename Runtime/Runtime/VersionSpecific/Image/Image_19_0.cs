using System;
using System.Runtime.InteropServices;

namespace Il2CppGen.Runtime.Runtime.VersionSpecific.Image;

[ApplicableToUnityVersionsSince("5.3.2")]
public unsafe class NativeImageStructHandler_19_0 : INativeImageStructHandler
{
    public int Size()
    {
        return sizeof(Il2CppImage_19_0);
    }

    public INativeImageStruct CreateNewStruct()
    {
        var ptr = Marshal.AllocHGlobal(Size());
        var _ = (Il2CppImage_19_0*)ptr;
        *_ = default;
        return new NativeStructWrapper(ptr);
    }

    public INativeImageStruct Wrap(Il2CppImage* ptr)
    {
        if (ptr == null) return null;
        return new NativeStructWrapper((IntPtr)ptr);
    }

    internal struct Il2CppImage_19_0
    {
        public byte* name;
        public int assemblyIndex;
        public int typeStart;
        public uint typeCount;
        public int entryPointIndex;
        public void* nameToClassHashTable;
        public uint token;
    }

    internal class NativeStructWrapper : INativeImageStruct
    {
        private byte _dynamicDummy;

        public NativeStructWrapper(IntPtr ptr)
        {
            Pointer = ptr;
        }

        private Il2CppImage_19_0* _ => (Il2CppImage_19_0*)Pointer;
        public IntPtr Pointer { get; }
        public Il2CppImage* ImagePointer => (Il2CppImage*)Pointer;
        public bool HasNameNoExt => false;
        public ref Il2CppAssembly* Assembly => throw new NotSupportedException();
        public ref byte Dynamic => ref _dynamicDummy;
        public ref IntPtr Name => ref *(IntPtr*)&_->name;
        public ref IntPtr NameNoExt => throw new NotSupportedException();
    }
}
