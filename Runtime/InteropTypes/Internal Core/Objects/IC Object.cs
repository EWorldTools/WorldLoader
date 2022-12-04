using System;
using System.Text;
using System.Linq;

namespace InternalCore.Objects // BlazeEng Bs
{
    //unsafe public class IE2Object<T> : IE2Object where T : unmanaged
    //{
    //    public IE2Object(IntPtr ptr) : base(ptr) { }
    //    public IE2Object(T value, IEClass type) : base(IntPtr.Zero)
    //    {
    //        Pointer = Import.Object.il2cpp_object_new(type.Pointer);
    //        if (Pointer == IntPtr.Zero)
    //            throw new NullReferenceException();

    //        *(T*)(Pointer + 0x10) = value;
    //    }

    //    /// <summary>
    //    ///     IS UNMANAGED
    //    /// </summary>
    //    /// <returns></returns>
    //    unsafe public T GetValue()
    //    {
    //        return *(T*)(Pointer + 0x10).ToPointer();
    //    }
    //}

    public class IE2Object
    {
        public IE2Object(IntPtr ptr) => Pointer = ptr;

        /// <summary>
        ///     NOT UNMANAGED
        /// </summary>
        /// <returns></returns>
        public T1 GetValue<T1>() where T1 : IE2Object
        {
            T1 obj = (T1)Activator.CreateInstance(typeof(T1), new object[] { IntPtr.Zero });
            obj.Pointer = Pointer;
            return obj;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return this == null;
            if (obj is IE2Object b) return b.Pointer == Pointer;
            return false;
        }
        public override int GetHashCode() => Pointer.GetHashCode();
        public static bool operator !=(IE2Object x, IE2Object y) => !(x == y);
        public static bool operator ==(IE2Object x, IE2Object y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.Pointer == y.Pointer;
        }
        ~IE2Object()
        {
            // For Clear RAM Pointer
            GC.SuppressFinalize(this);
        }

        public bool Static
        {
            get => handleStatic > 0;
            set
            {
                if (value)
                {
                    if (handleStatic < 1)
                    {
                        handleStatic = Import.Handler.il2cpp_gchandle_new(Pointer, true);
                    }
                }
                else
                {
                    if (handleStatic > 0)
                    {
                        Import.Handler.il2cpp_gchandle_free(handleStatic);
                        handleStatic = 0;
                    }
                }
            }
        }

        public IntPtr Pointer;

        private uint handleStatic = 0;
    }
}
