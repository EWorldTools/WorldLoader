using Il2CppGen.Runtime.WCPatch;
using InternalCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Il2CppGen.Runtime.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class WLPatch : Attribute
{
    public delegate void _Patch(IntPtr instance);
    public static _Patch __Patch;
    public static IE2Method ThisMethod;

    public WLPatch(IntPtr method) {
        __Patch = PatchUtils.Patch<_Patch>(method, Detor);

    }

    public static void Detor(IntPtr instance)
    {
        try {
            ThisMethod.Invoke();
        }
        finally {
            if (instance != IntPtr.Zero)
                __Patch(instance);
        }
    }
}

