using System;

namespace InternalCore.Objects
{
    public class IE2ClassType : IE2Object
    {
        public IE2ClassType(IntPtr ptr) : base(ptr) { }

        public string Name
        {
            get => Import.Object.il2cpp_type_get_name(Pointer);
        }
    }
}
