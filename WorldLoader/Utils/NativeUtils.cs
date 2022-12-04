using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WorldLoader.HookUtils
{
    public static class NativeUtils
    {
        private static readonly List<object> PinnedDelegates = new List<object>();

        public static T CreateDelegate<T>(IntPtr method) where T : Delegate {
            Logs.Log($"Making Delegate for {method}", "MinHook");
            return Marshal.GetDelegateForFunctionPointer(method, typeof(T)) as T;
        }

        public static T CreateDelegate<T>(IntPtr method, out T Outdelegate) where T : Delegate {
            Logs.Log($"Making Delegate for {method}", "MinHook");
            return Outdelegate = Marshal.GetDelegateForFunctionPointer(method, typeof(T)) as T;
        }

        public static unsafe T Detour<T>(IntPtr @from, T to) where T : Delegate {
            IntPtr* targetVarPointer = &from;
            PinnedDelegates.Add(to);
            MinHook.CreateHook(*targetVarPointer, Marshal.GetFunctionPointerForDelegate(to), out var OriginalMethod);
            //MinHook.EnableHook(@from);
            return Marshal.GetDelegateForFunctionPointer<T>(from);
        }

        public static unsafe T Detour<T>(IntPtr @from, T to, out IntPtr OriginalMethod) where T : Delegate {
            IntPtr* targetVarPointer = &from;
            PinnedDelegates.Add(to);
            MinHook.CreateHook(*targetVarPointer, Marshal.GetFunctionPointerForDelegate(to), out OriginalMethod);
            //MinHook.EnableHook(@from);
            return Marshal.GetDelegateForFunctionPointer<T>(from);
        }
    }
}
