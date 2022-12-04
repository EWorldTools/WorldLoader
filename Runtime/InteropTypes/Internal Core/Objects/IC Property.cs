using System;
using System.Runtime.InteropServices;

namespace InternalCore.Objects
{
    public class IE2Property : IE2Object
    {
        internal IE2Property(IntPtr ptr) : base(ptr) { }

        private string szName;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(szName))
                    szName = Marshal.PtrToStringAnsi(Import.Property.il2cpp_property_get_name(Pointer));
                return szName;
            }
            set
            {
                szName = value;
                if (GetGetMethod() != null)
                    GetGetMethod().Name = "get_" + value;
                if (GetSetMethod() != null)
                    GetSetMethod().Name = "set_" + value;
            }
        }

        public string OriginalName => Marshal.PtrToStringAnsi(Import.Property.il2cpp_property_get_name(Pointer));

        public IL2BindingFlags Flags => (IL2BindingFlags)Import.Property.il2cpp_property_get_flags(Pointer);
        public bool HasFlag(IL2BindingFlags flag) => ((Flags & flag) != 0);
        public bool Instance => GetGetMethod() != null && GetGetMethod().Instance;

        public IE2Method GetGetMethod()
        {
            if (getMethod == null)
            {
                IntPtr method = Import.Property.il2cpp_property_get_get_method(Pointer);
                if (method != IntPtr.Zero)
                    getMethod = new IE2Method(method);
            }
            return getMethod;
        }
        private IE2Method getMethod;
        public IE2Method GetSetMethod()
        {
            if (setMethod == null)
            {
                IntPtr method = Import.Property.il2cpp_property_get_set_method(Pointer);
                if (method != IntPtr.Zero)
                    setMethod = new IE2Method(method);
            }
            return setMethod;
        }
        private IE2Method setMethod;

        public bool IsStatic => GetGetMethod()?.IsStatic == true || GetSetMethod()?.IsStatic == true;
        public bool IsPublic => GetGetMethod()?.IsPublic == true || GetSetMethod()?.IsPublic == true;
    }
}