using System;
using System.Text;
using InternalCore.Objects;

namespace IeObjectString;

public class IEObjString : IE2Object // BlazeEng Bs
{
    public IEObjString(IntPtr ptr) : base(ptr) { }

    unsafe public override string ToString() {
        if (Pointer == IntPtr.Zero)
            return null;

        return new string((char*)Pointer + 10);
    }
}

public class IE2String_utf8 : IEObjString
{
    public IE2String_utf8(IntPtr ptr) : base(ptr) { }
    unsafe public IE2String_utf8(string value) : base(IntPtr.Zero) {
        if (value == null) {
            Pointer = IntPtr.Zero;
            return;
        }
        Pointer = Import.Object.il2cpp_string_new(value);
    }
}

public class IE2String_utf16 : IEObjString
{
    public IE2String_utf16(IntPtr ptr) : base(ptr) { }
    unsafe public IE2String_utf16(string value) : base(IntPtr.Zero) {
        Pointer = IntPtr.Zero;
        if (value == null) return;
        while (Pointer == IntPtr.Zero || ToString() != value) {
            int length = value.Length;
            Pointer = Import.Object.il2cpp_string_new(string.Empty.PadRight(length, '\u0001'));
            for (int i = 0; i < length; i++)
                *(char*)(Pointer + 0x14 + (0x2 * i)) = value[i];
        }
    }
}
