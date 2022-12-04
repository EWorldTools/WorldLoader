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
        public static class HookInprts {
            public static IntPtr mVRC_CreateHook { get; set; }
            public static IntPtr mVRC_RemoveHook { get; set; }
            public static IntPtr mVRC_EnableHook { get; set; }
            public static IntPtr mVRC_DisableHook { get; set; }
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