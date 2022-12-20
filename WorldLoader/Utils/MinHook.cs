using HarmonyLib;
using MonoMod.RuntimeDetour;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using WorldLoader.HookUtils;

namespace WorldLoader.HookUtils
{
    public static class MinHook
    {
        /// <summary>
        /// In case u wanted to make your own MinHook Ins
        /// </summary>
        public static class HookInprts {
            public static IntPtr CreateHook { get; set; }
            public static IntPtr RemoveHook { get; set; }
            public static IntPtr EnableHook { get; set; }
            public static IntPtr DisableHook { get; set; }
        }

        // (LPVOID pTarget, LPVOID pDetour, LPVOID* ppOrig)
        public delegate void _CreateHook(IntPtr pTarget, IntPtr pDetour, out IntPtr ppOrig);
        public delegate void _RemoveHook(IntPtr pTarget);
        public delegate void _EnableHook(IntPtr pTarget);
        public delegate void _DisableHook(IntPtr pTarget);

        public static _CreateHook CreateHook { get; internal set; }
        public static _RemoveHook RemoveHook { get; internal set; }
        public static _EnableHook EnableHook { get; internal set; }
        public static _DisableHook DisableHook { get; internal set; }

    }
}