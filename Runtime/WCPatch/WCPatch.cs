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
    unsafe public class IL2CppPatch
    {
        public IntPtr Pointer { get; private set; }
        public IntPtr TargetMethod { get; private set; }
        public IntPtr OriginalMethod { get; private set; }
        private bool isEnabled = true;

        public IL2CppPatch(IntPtr targetMethod, IntPtr newMethod) {
            Pointer = newMethod;
            TargetMethod = targetMethod;

            MinHook.CreateHook(targetMethod, Pointer, out var OgMethod);
            OriginalMethod = OgMethod;
            Logs.Debug("[IL2CppPatch] " + Regex.Replace(Marshal.PtrToStringAnsi(targetMethod), @"[^\u0000-\u007F]+", "?").Replace("\\", string.Empty) + " => " + Marshal.PtrToStringAnsi(newMethod));
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

        public T GetDelegate<T>() where T : Delegate =>
             Marshal.GetDelegateForFunctionPointer(OriginalMethod, typeof(T)) as T;
        
        public bool Active
        {
            get => isEnabled;
            set {
                if (isEnabled = value)
                    MinHook.EnableHook(*(IntPtr*)TargetMethod);
                else
                    MinHook.DisableHook(*(IntPtr*)TargetMethod);
            }
        }
    }
}
