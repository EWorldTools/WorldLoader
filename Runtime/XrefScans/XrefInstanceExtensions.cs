using System.Reflection;
using System.Runtime.InteropServices;
using WorldLoader.Il2CppGen.Internal.XrefScans;
using Il2CppSystem;
using IntPtr = System.IntPtr;
using InvalidOperationException = System.InvalidOperationException;

namespace Il2CppGen.Runtime.XrefScans;

public static class XrefInstanceExtensions
{
    public static Object? ReadAsObject(this XrefInstance self)
    {
        if (self.Type != XrefType.Global) throw new InvalidOperationException("Can't read non-global xref as object");

        var valueAtPointer = Marshal.ReadIntPtr(self.Pointer);
        if (valueAtPointer == IntPtr.Zero)
            return null;

        return new Object(valueAtPointer);
    }

    public static MethodBase TryResolve(this XrefInstance self)
    {
        if (self.Type != XrefType.Method) throw new InvalidOperationException("Can't resolve non-method xrefs");

        return XrefScanMethodDb.TryResolvePointer(self.Pointer);
    }
}
