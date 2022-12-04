using System;
using System.Runtime.InteropServices;

namespace Il2CppGen.Runtime.Runtime.VersionSpecific.FieldInfo;

[ApplicableToUnityVersionsSince("5.2.2")]
public unsafe class NativeFieldInfoStructHandler_16_0 : INativeFieldInfoStructHandler
{
    public int Size()
    {
        return sizeof(Il2CppFieldInfo_16_0);
    }

    public INativeFieldInfoStruct CreateNewStruct()
    {
        var ptr = Marshal.AllocHGlobal(Size());
        var _ = (Il2CppFieldInfo_16_0*)ptr;
        *_ = default;
        return new NativeStructWrapper(ptr);
    }

    public INativeFieldInfoStruct Wrap(Il2CppFieldInfo* ptr)
    {
        if (ptr == null) return null;
        return new NativeStructWrapper((IntPtr)ptr);
    }

    internal struct Il2CppFieldInfo_16_0
    {
        public byte* name;
        public Il2CppTypeStruct* type;
        public Il2CppClass* parent;
        public int offset;
        public int customAttributeIndex;
    }

    internal class NativeStructWrapper : INativeFieldInfoStruct
    {
        public NativeStructWrapper(IntPtr ptr)
        {
            Pointer = ptr;
        }

        private Il2CppFieldInfo_16_0* _ => (Il2CppFieldInfo_16_0*)Pointer;
        public IntPtr Pointer { get; }
        public Il2CppFieldInfo* FieldInfoPointer => (Il2CppFieldInfo*)Pointer;
        public ref IntPtr Name => ref *(IntPtr*)&_->name;
        public ref Il2CppTypeStruct* Type => ref _->type;
        public ref Il2CppClass* Parent => ref _->parent;
        public ref int Offset => ref _->offset;
    }
}
