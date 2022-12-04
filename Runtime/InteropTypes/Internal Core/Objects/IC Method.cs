using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace InternalCore.Objects
{
    public class IE2Method : IE2Object
    {
        public IE2Method(IntPtr ptr) : base(ptr) { }

        private string szName;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(szName))
                    szName = OriginalName;
                return szName;
            }
            set => szName = value;
        }

        public string OriginalName => Marshal.PtrToStringAnsi(Import.Method.il2cpp_method_get_name(Pointer));

        public int Token => Import.Method.il2cpp_method_get_token(Pointer);
        public bool IsAbstract => HasFlag(IL2BindingFlags.METHOD_ABSTRACT);
        public bool IsVirtual => HasFlag(IL2BindingFlags.METHOD_VIRTUAL);
        public bool IsStatic => HasFlag(IL2BindingFlags.METHOD_STATIC);
        public bool IsPrivate => HasFlag(IL2BindingFlags.METHOD_PRIVATE);
        public bool IsPublic => HasFlag(IL2BindingFlags.METHOD_PUBLIC);

        public bool Instance => IsStatic && GetParameters().Length == 0 && ReturnType.Name == ReflectedType.FullName;

        public IEClass ReflectedType => new IEClass(Import.Method.il2cpp_method_get_class(Pointer));

        public IE2ClassType ReturnType => new IE2ClassType(Import.Method.il2cpp_method_get_return_type(Pointer));

        public IE2Param[] GetParameters()
        {
            if (Parameters == null)
            {
                Parameters = new List<IE2Param>();
                uint param_count = Import.Method.il2cpp_method_get_param_count(Pointer);
                for (uint i = 0; i < param_count; i++)
                    Parameters.Add(new IE2Param(Import.Method.il2cpp_method_get_param(Pointer, i), Marshal.PtrToStringAnsi(Import.Method.il2cpp_method_get_param_name(Pointer, i))));
            }

            return Parameters.ToArray();
        }
        private List<IE2Param> Parameters = null;


        public bool HasAttribute(IEClass klass)
        {
            if (klass == null) return false;
            return Import.Method.il2cpp_method_has_attribute(Pointer, klass.Pointer);
        }

        public IL2BindingFlags Flags
        {
            get
            {
                uint f = 0;
                return (IL2BindingFlags)Import.Method.il2cpp_method_get_flags(Pointer, ref f);
            }
        }

        public bool HasFlag(IL2BindingFlags flag) => ((Flags & flag) != 0);

        public IE2Method ToVirtual(IE2Object obj = null)
        {
            IntPtr result = Import.Method.il2cpp_object_get_virtual_method(obj == null ? IntPtr.Zero : obj.Pointer, Pointer);
            if (result == IntPtr.Zero)
                return null;

            return new IE2Method(result);
        }

        public IE2Object Invoke() => Invoke(IntPtr.Zero, new IntPtr[] { IntPtr.Zero });
        public IE2Object Invoke(IE2Object obj, bool ex = true) => Invoke(obj.Pointer, new IntPtr[] { IntPtr.Zero }, ex: ex);
        public IE2Object Invoke(IntPtr obj, bool ex = true) => Invoke(obj, new IntPtr[] { IntPtr.Zero }, ex: ex);
        public IE2Object Invoke(params IntPtr[] paramtbl)
        {
            return Invoke(IntPtr.Zero, paramtbl);
        }
        public IE2Object Invoke(IE2Object obj, IntPtr[] paramtbl, bool ex = true) => Invoke(obj.Pointer, paramtbl, ex);
        public IE2Object Invoke(IntPtr obj, IntPtr[] paramtbl, bool ex = true)
        {
            IntPtr returnval = InvokeMethod(Pointer, obj, paramtbl, IsVirtual, ex);
            if (returnval != IntPtr.Zero)
                return new IE2Object(returnval);
            return null;
        }

        public IE2Object<T> Invoke<T>() where T : unmanaged => Invoke<T>(IntPtr.Zero, new IntPtr[] { IntPtr.Zero });
        public IE2Object<T> Invoke<T>(IE2Object obj, bool ex = true) where T : unmanaged => Invoke<T>(obj.Pointer, new IntPtr[] { IntPtr.Zero }, ex: ex);
        public IE2Object<T> Invoke<T>(IntPtr obj, bool ex = true) where T : unmanaged => Invoke<T>(obj, new IntPtr[] { IntPtr.Zero }, ex: ex);
        public IE2Object<T> Invoke<T>(params IntPtr[] paramtbl) where T : unmanaged
        {
            return Invoke<T>(IntPtr.Zero, paramtbl);
        }
        public IE2Object<T> Invoke<T>(IE2Object obj, IntPtr[] paramtbl, bool ex = true) where T : unmanaged => Invoke<T>(obj.Pointer, paramtbl, ex);
        public IE2Object<T> Invoke<T>(IntPtr obj, IntPtr[] paramtbl, bool ex = true) where T : unmanaged
        {
            IntPtr returnval = InvokeMethod(Pointer, obj, paramtbl, IsVirtual, ex);
            if (returnval != IntPtr.Zero)
                return new IE2Object<T>(returnval);
            return null;
        }

        unsafe public static IntPtr InvokeMethod(IntPtr method, IntPtr obj, IntPtr[] paramtbl, bool isVirtual = false, bool ex = true)
        {
            if (method == IntPtr.Zero)
                return IntPtr.Zero;
            IntPtr[] intPtrArray;
            IntPtr returnval = IntPtr.Zero;
            intPtrArray = ((paramtbl != null) ? paramtbl : new IntPtr[0]);
            IntPtr intPtr = Marshal.AllocHGlobal(intPtrArray.Length * sizeof(void*));
            try
            {
                void** pointerArray = (void**)intPtr.ToPointer();
                for (int i = 0; i < intPtrArray.Length; i++)
                    pointerArray[i] = intPtrArray[i].ToPointer();

                IntPtr @m = isVirtual ? Import.Method.il2cpp_object_get_virtual_method(obj, method) : method;

                IntPtr err = IntPtr.Zero;
                returnval = Import.Method.il2cpp_runtime_invoke(@m, obj, pointerArray, new IntPtr(&err));
                if (err != IntPtr.Zero && ex)
                {
                    Console.WriteLine("Error: " + Import.BuildMessage(err));
                    Console.WriteLine("Src: " + new IE2Method(@m).Name);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(intPtr);
            }
            return returnval;
        }
    }
}
