﻿using System.Diagnostics;
using System.Linq;
using WorldLoader.Il2CppGen.Internal.XrefScans;

namespace Il2CppGen.Runtime;

internal class MemoryUtils
{
    public static nint FindSignatureInModule(ProcessModule module, SignatureDefinition sigDef)
    {
        var ptr = FindSignatureInBlock(
            module.BaseAddress,
            module.ModuleMemorySize,
            sigDef.pattern,
            sigDef.mask,
            sigDef.offset
        );
        if (ptr != 0 && sigDef.xref)
            ptr = XrefScannerLowLevel.JumpTargets(ptr).FirstOrDefault();
        return ptr;
    }

    public static nint FindSignatureInBlock(nint block, long blockSize, string pattern, string mask, long sigOffset = 0)
    {
        return FindSignatureInBlock(block, blockSize, pattern.ToCharArray(), mask.ToCharArray(), sigOffset);
    }

    public static unsafe nint FindSignatureInBlock(nint block, long blockSize, char[] pattern, char[] mask,
        long sigOffset = 0)
    {
        for (long address = 0; address < blockSize; address++)
        {
            var found = true;
            for (uint offset = 0; offset < mask.Length; offset++)
                if (*(byte*)(address + block + offset) != (byte)pattern[offset] && mask[offset] != '?')
                {
                    found = false;
                    break;
                }

            if (found)
                return (nint)(address + block + sigOffset);
        }

        return 0;
    }

    public struct SignatureDefinition
    {
        public string pattern;
        public string mask;
        public int offset;
        public bool xref;
    }
}
