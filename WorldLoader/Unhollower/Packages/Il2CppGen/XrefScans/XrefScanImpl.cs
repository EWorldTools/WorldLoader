using System;
using WorldLoader.Il2CppGen.Internal.XrefScans;

namespace WorldLoader.Il2CppGen.Generator.XrefScans;

internal class XrefScanImpl : IXrefScannerImpl
{
    public (XrefScanUtil.InitMetadataForMethod, IntPtr)? GetMetadataResolver()
    {
        return null;
    }

    public bool XrefGlobalClassFilter(IntPtr movTarget)
    {
        return false;
    }
}
