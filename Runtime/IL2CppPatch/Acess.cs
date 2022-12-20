using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.HookUtils;

namespace Il2CppGen.Runtime.IL2CppPatch;

public class Acess
{
    private bool isEnabled = true;

    public IntPtr Pointer { get; internal set; }
    public IntPtr TargetMethod { get; internal set; }
    public IntPtr OriginalMethod { get; internal set; }

    public unsafe bool Active
    {
        get => isEnabled;
        set
        {
            if (isEnabled = value)
                MinHook.EnableHook(*(IntPtr*)TargetMethod);
            else
                MinHook.DisableHook(*(IntPtr*)TargetMethod);
        }
    }
}

