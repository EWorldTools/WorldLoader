using System;

namespace WorldLoader.Il2CppGen.Internal.XrefScans;

public interface IXrefScannerImpl
{
    (XrefScanUtil.InitMetadataForMethod, IntPtr)? GetMetadataResolver();

    bool XrefGlobalClassFilter(IntPtr movTarget);
}
