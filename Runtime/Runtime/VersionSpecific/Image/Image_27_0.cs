using System;
using System.Runtime.InteropServices;

namespace Il2CppGen.Runtime.Runtime.VersionSpecific.Image;

[ApplicableToUnityVersionsSince("2020.2.0")]
public unsafe class NativeImageStructHandler_27_0 : INativeImageStructHandler
{
    public int Size()
    {
        return sizeof(Il2CppImage_27_0);
    }

    public INativeImageStruct CreateNewStruct()
    {
        var ptr = Marshal.AllocHGlobal(Size());
        var _ = (Il2CppImage_27_0*)ptr;
        *_ = default;
        var metadata = (Il2CppImageGlobalMetadata*)Marshal.AllocHGlobal(sizeof(Il2CppImageGlobalMetadata));
        metadata->image = (Il2CppImage*)_;
        *(Il2CppImageGlobalMetadata**)&_->metadataHandle = metadata;
        return new NativeStructWrapper(ptr);
    }

    public INativeImageStruct Wrap(Il2CppImage* ptr)
    {
        if (ptr == null) return null;
        return new NativeStructWrapper((IntPtr)ptr);
    }

    internal struct Il2CppImage_27_0
    {
        public byte* name;
        public byte* nameNoExt;
        public Il2CppAssembly* assembly;
        public uint typeCount;
        public uint exportedTypeCount;
        public uint customAttributeCount;
        public Il2CppMetadataImageHandle metadataHandle;
        public void* nameToClassHashTable;
        public void* codeGenModule;
        public uint token;
        public byte dynamic;
    }

    internal class NativeStructWrapper : INativeImageStruct
    {
        public NativeStructWrapper(IntPtr ptr)
        {
            Pointer = ptr;
        }

        private Il2CppImage_27_0* _ => (Il2CppImage_27_0*)Pointer;
        public IntPtr Pointer { get; }
        public Il2CppImage* ImagePointer => (Il2CppImage*)Pointer;
        public bool HasNameNoExt => true;
        public ref Il2CppAssembly* Assembly => ref _->assembly;
        public ref byte Dynamic => ref _->dynamic;
        public ref IntPtr Name => ref *(IntPtr*)&_->name;
        public ref IntPtr NameNoExt => ref *(IntPtr*)&_->nameNoExt;
    }
}
