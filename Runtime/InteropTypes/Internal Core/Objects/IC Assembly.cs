using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace InternalCore.Objects
{
    public class IE2Assembly : IE2Object
    {

        public string Name { get; private set; }
        private List<IEClass> ClassList = new List<IEClass>();
        public IE2Assembly(IntPtr ptr) : base(ptr)
        {
            Pointer = ptr;
            Name = Path.GetFileNameWithoutExtension(Marshal.PtrToStringAnsi(Import.Image.il2cpp_image_get_name(Pointer)));

            // Map out Classes
            uint count = Import.Image.il2cpp_image_get_class_count(Pointer);
            for (uint i = 0; i < count; i++)
            {
                IEClass klass = new IEClass(Import.Image.il2cpp_image_get_class(Pointer, i));
                if (klass.DeclaringType == null)
                    ClassList.Add(klass);
            }
        }
        public IEClass[] GetClasses() => ClassList.ToArray();
        public IEClass[] GetClasses(IL2BindingFlags flags) => GetClasses().Where(x => x.HasFlag(flags)).ToArray();
        public IEClass GetClass(IntPtr ptr) => GetClasses().Where(x => x.Pointer == ptr).FirstOrDefault();
        public IEClass GetClass(string name) => GetClass(name, null);
        public IEClass GetClass(string name, IL2BindingFlags flags) => GetClass(name, null, flags);
        public IEClass GetClass(string name, string name_space)
        {
            IEClass returnval = null;
            foreach (IEClass type in GetClasses())
            {
                if (type.Name.Equals(name) && (string.IsNullOrEmpty(type.Namespace) || type.Namespace.Equals(name_space)))
                {
                    returnval = type;
                    break;
                }
                else
                {
                    foreach (IEClass nestedtype in type.GetNestedTypes())
                    {
                        if (nestedtype.Name.Equals(name) && (string.IsNullOrEmpty(nestedtype.Namespace) || nestedtype.Namespace.Equals(name_space)))
                        {
                            returnval = nestedtype;
                            break;
                        }
                    }
                    if (returnval != null)
                        break;
                }
            }
            return returnval;
        }
        public IEClass GetClass(string name, string name_space, IL2BindingFlags flags)
        {
            IEClass returnval = null;
            foreach (IEClass type in GetClasses())
            {
                if (type.Name.Equals(name) && (string.IsNullOrEmpty(type.Namespace) || type.Namespace.Equals(name_space)) && type.HasFlag(flags))
                {
                    returnval = type;
                    break;
                }
                /*
                else
                {
                    foreach (IEClass nestedtype in type.GetNestedTypes())
                    {
                        if (nestedtype.Name.Equals(name) && (string.IsNullOrEmpty(nestedtype.Namespace) || nestedtype.Namespace.Equals(name_space)) && nestedtype.HasFlag(flags))
                        {
                            returnval = nestedtype;
                            break;
                        }
                    }
                    if (returnval != null)
                        break;
                }
                */
            }
            return returnval;
        }
    }
}
