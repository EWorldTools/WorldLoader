using InternalCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorldLoader.HookUtils;

namespace Il2CppGen.Runtime.IL2CppPatch
{
    unsafe public class IL2CppPatch : Acess // idea From Blaze, Edited a little (this is pretty base line code tho)
    {
        public IL2CppPatch(IntPtr targetMethod, IntPtr newMethod) {
            MinHook.CreateHook(targetMethod, newMethod, out var OgMethod);
            Pointer = newMethod;
            TargetMethod = targetMethod;
            OriginalMethod = OgMethod;
            Active = true;
        }

        public IL2CppPatch(MethodBase targetMethod, Delegate newMethod) 
            : this(targetMethod.MethodHandle.GetFunctionPointer(), newMethod.Method.MethodHandle.GetFunctionPointer()) {
        }

        public IL2CppPatch(MethodBase targetMethod, MethodBase newMethod)
            : this(targetMethod.MethodHandle.GetFunctionPointer(), newMethod.MethodHandle.GetFunctionPointer())
        {
        }

        public IL2CppPatch(IntPtr targetMethod, Delegate newMethod)
            : this(targetMethod, newMethod.Method.MethodHandle.GetFunctionPointer())
        {
        }

        public IL2CppPatch(IntPtr targetMethod, MethodBase newMethod)
            : this(targetMethod, newMethod.MethodHandle.GetFunctionPointer())
        {
        }
    }
}
