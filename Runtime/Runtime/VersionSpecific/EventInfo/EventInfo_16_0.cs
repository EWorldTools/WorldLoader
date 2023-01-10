using System;
using System.Runtime.InteropServices;

namespace Il2CppInterop.Runtime.Runtime.VersionSpecific.EventInfo;

[ApplicableToUnityVersionsSince("5.2.2")]
public unsafe class NativeEventInfoStructHandler_16_0 : INativeEventInfoStructHandler
{
    public int Size()
    {
        return sizeof(Il2CppEventInfo_16_0);
    }

    public INativeEventInfoStruct CreateNewStruct()
    {
        var ptr = Marshal.AllocHGlobal(Size());
        var _ = (Il2CppEventInfo_16_0*)ptr;
        *_ = default;
        return new NativeStructWrapper(ptr);
    }

    public INativeEventInfoStruct Wrap(Il2CppEventInfo* ptr)
    {
        if (ptr == null) return null;
        return new NativeStructWrapper((IntPtr)ptr);
    }

    internal struct Il2CppEventInfo_16_0
    {
        public byte* name;
        public Il2CppTypeStruct* eventType;
        public Il2CppClass* parent;
        public Il2CppMethodInfo* add;
        public Il2CppMethodInfo* remove;
        public Il2CppMethodInfo* raise;
        public int customAttributeIndex;
    }

    internal class NativeStructWrapper : INativeEventInfoStruct
    {
        public NativeStructWrapper(IntPtr ptr)
        {
            Pointer = ptr;
        }

        private Il2CppEventInfo_16_0* _ => (Il2CppEventInfo_16_0*)Pointer;
        public IntPtr Pointer { get; }
        public Il2CppEventInfo* EventInfoPointer => (Il2CppEventInfo*)Pointer;
        public ref IntPtr Name => ref *(IntPtr*)&_->name;
        public ref Il2CppTypeStruct* EventType => ref _->eventType;
        public ref Il2CppClass* Parent => ref _->parent;
        public ref Il2CppMethodInfo* Add => ref _->add;
        public ref Il2CppMethodInfo* Remove => ref _->remove;
        public ref Il2CppMethodInfo* Raise => ref _->raise;
    }
}
