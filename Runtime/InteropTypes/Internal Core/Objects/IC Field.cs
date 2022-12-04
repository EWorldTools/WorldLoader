using System;
using System.Runtime.InteropServices;

namespace InternalCore.Objects
{
    public class IE2Field : IE2Object
    {
        internal IE2Field(IntPtr ptr) : base(ptr) { }

        private string szName;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(szName))
                    szName = Marshal.PtrToStringAnsi(Import.Field.il2cpp_field_get_name(Pointer));
                return szName;
            }
            set => szName = value;
        }

        public IE2ClassType ReturnType => new IE2ClassType(Import.Field.il2cpp_field_get_type(Pointer));
        public int Token => Import.Field.il2cpp_field_get_offset(Pointer);

        public IL2BindingFlags Flags
        {
            get
            {
                uint flags = 0;
                return (IL2BindingFlags)Import.Field.il2cpp_field_get_flags(Pointer, ref flags);
            }
        }
        public bool HasFlag(IL2BindingFlags flag) => ((Flags & flag) != 0);

        public bool IsStatic => HasFlag(IL2BindingFlags.FIELD_STATIC);
        public bool IsPrivate => HasFlag(IL2BindingFlags.FIELD_PRIVATE);
        public bool IsPublic => HasFlag(IL2BindingFlags.FIELD_PUBLIC);

        public bool Instance => IsStatic && ReturnType.Name == ReflectedType.FullName;
        public IEClass ReflectedType => new IEClass(Import.Field.il2cpp_field_get_parent(Pointer));

        public bool HasAttribute(IEClass klass)
        {
            if (klass == null) return false;
            return Import.Field.il2cpp_field_has_attribute(Pointer, klass.Pointer);
        }

        public IE2Object GetValue() => GetValue(IntPtr.Zero);
        public IE2Object GetValue(IE2Object obj) => GetValue(obj.Pointer);
        public IE2Object GetValue(IntPtr obj)
        {
            IntPtr returnval = IntPtr.Zero;
            if (HasFlag(IL2BindingFlags.FIELD_STATIC))
                returnval = Import.Field.il2cpp_field_get_value_object(Pointer, IntPtr.Zero);
            else
                returnval = Import.Field.il2cpp_field_get_value_object(Pointer, obj);
            if (returnval != IntPtr.Zero)
                return new IE2Object(returnval);
            return null;
        }
        public new IE2Object<T> GetValue<T>() where T : unmanaged => GetValue<T>(IntPtr.Zero);
        public IE2Object<T> GetValue<T>(IE2Object obj) where T : unmanaged => GetValue<T>(obj.Pointer);
        public IE2Object<T> GetValue<T>(IntPtr obj) where T : unmanaged
        {
            IntPtr returnval = IntPtr.Zero;
            if (HasFlag(IL2BindingFlags.FIELD_STATIC))
                returnval = Import.Field.il2cpp_field_get_value_object(Pointer, IntPtr.Zero);
            else
                returnval = Import.Field.il2cpp_field_get_value_object(Pointer, obj);
            if (returnval != IntPtr.Zero)
                return new IE2Object<T>(returnval);
            return null;
        }
        public void SetValue(IE2Object value) => SetValue(IntPtr.Zero, value.Pointer);
        public void SetValue(IntPtr value) => SetValue(IntPtr.Zero, value);
        public void SetValue(IE2Object obj, IntPtr value) => SetValue(obj.Pointer, value);
        public void SetValue(IntPtr obj, IntPtr value)
        {
            if (HasFlag(IL2BindingFlags.FIELD_STATIC))
                Import.Field.il2cpp_field_static_set_value(Pointer, value);
            else
                Import.Field.il2cpp_field_set_value(obj, Pointer, value);
        }
    }
}
