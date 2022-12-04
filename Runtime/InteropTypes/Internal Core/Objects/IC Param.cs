using System;

namespace InternalCore.Objects
{
    public class IE2Param : IE2Object
    {
        public string Name { get; private set; }
        internal IE2Param(IntPtr ptr, string name) : base(ptr)
        {
            Pointer = ptr;
            Name = name;
        }
        public IE2ClassType ReturnType => new IE2ClassType(Pointer);
    }
}
