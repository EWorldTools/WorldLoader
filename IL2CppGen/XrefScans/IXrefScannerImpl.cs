using System;

namespace Il2CppInterop.Internal.XrefScans;

public interface IXrefScannerImpl
{
    (XrefScanUtil.InitMetadataForMethod, IntPtr)? GetMetadataResolver();

    bool XrefGlobalClassFilter(IntPtr movTarget);
}
