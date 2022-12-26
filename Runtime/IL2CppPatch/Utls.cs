using InternalCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.HookUtils;
using WorldLoader.Il2CppGen.Internal;

namespace Il2CppGen.Runtime.IL2CppPatch;

public static class QuickPatch
{
    public static unsafe TDelegate FastHook<TDelegate>(MethodInfo targetMethod, MethodInfo patch, Action AfterHook) where TDelegate : Delegate
    {
        try {
            var method = *(IntPtr*)(IntPtr)Il2CppGenUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(targetMethod).GetValue(null);
            new IL2CppPatch((IntPtr)(&method), patch!.MethodHandle.GetFunctionPointer());
            return Marshal.GetDelegateForFunctionPointer<TDelegate>(method);
        }
        finally {
            AfterHook();
        }
    }
}