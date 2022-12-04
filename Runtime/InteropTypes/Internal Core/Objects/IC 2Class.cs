using IeObjectString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace InternalCore.Objects
{
    public class IEClass : IE2Object
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(Namespace))
                    return Name;

                return Namespace + "." + Name;
            }
        }
        private List<IE2Method> MethodList = new List<IE2Method>();
        private List<IE2Field> FieldList = new List<IE2Field>();
        //private List<IL2CPP_Event> EventList = new List<IL2CPP_Event>();
        private List<IEClass> NestedTypeList = new List<IEClass>();
        private List<IEClass> InterfaceTypeList = new List<IEClass>();
        private List<IE2Property> PropertyList = new List<IE2Property>();
        public IEClass(IntPtr ptr) : base(ptr)
        {
            Pointer = ptr;
            Name = Marshal.PtrToStringAnsi(Import.Class.il2cpp_class_get_name(ptr));
            Namespace = Marshal.PtrToStringAnsi(Import.Class.il2cpp_class_get_namespace(ptr));
            // Name = Import.StringFromNativeUtf8(Import.Class.il2cpp_class_get_name(ptr));
            //Namespace = Import.StringFromNativeUtf8(Import.Class.il2cpp_class_get_namespace(ptr));

            // Find Methods
            IntPtr method_iter = IntPtr.Zero;
            IntPtr method = IntPtr.Zero;
            while ((method = Import.Class.il2cpp_class_get_methods(ptr, ref method_iter)) != IntPtr.Zero)
                MethodList.Add(new IE2Method(method));

            // Find Fields
            IntPtr field_iter = IntPtr.Zero;
            IntPtr field = IntPtr.Zero;
            while ((field = Import.Class.il2cpp_class_get_fields(ptr, ref field_iter)) != IntPtr.Zero)
                FieldList.Add(new IE2Field(field));
            /*

            // Map out Events
            IntPtr evt_iter = IntPtr.Zero;
            IntPtr evt = IntPtr.Zero;
            while ((evt = IL2CPP.il2cpp_class_get_events(Ptr, ref evt_iter)) != IntPtr.Zero)
                EventList.Add(new IL2CPP_Event(evt));

            */
            // Find Nested Class
            IntPtr nestedtype_iter = IntPtr.Zero;
            IntPtr nestedtype = IntPtr.Zero;
            while ((nestedtype = Import.Class.il2cpp_class_get_nested_types(ptr, ref nestedtype_iter)) != IntPtr.Zero)
                NestedTypeList.Add(new IEClass(nestedtype));

            IntPtr interfacetype_iter = IntPtr.Zero;
            IntPtr interfacetype = IntPtr.Zero;
            while ((interfacetype = Import.Class.il2cpp_class_get_interfaces(ptr, ref interfacetype_iter)) != IntPtr.Zero)
                InterfaceTypeList.Add(new IEClass(interfacetype));

            // Find Property
            IntPtr property_iter = IntPtr.Zero;
            IntPtr property = IntPtr.Zero;
            while ((property = Import.Class.il2cpp_class_get_properties(ptr, ref property_iter)) != IntPtr.Zero)
            {
                IE2Property p = new IE2Property(property);
                PropertyList.Add(p);
                if (p.GetGetMethod() != null)
                    MethodList.Remove(p.GetGetMethod());
                if (p.GetSetMethod() != null)
                    MethodList.Remove(p.GetSetMethod());
            }
        }

        public int Token => Import.Class.il2cpp_class_get_type_token(Pointer);

        public IE2Assembly Assembly
        {
            get
            {
                IntPtr pointer = Import.Class.il2cpp_class_get_image(Pointer);
                if (pointer != IntPtr.Zero)
                    return new IE2Assembly(pointer);

                return null;
            }
        }

        public IEClass DeclaringType
        {
            get
            {
                IntPtr pointer = Import.Class.il2cpp_class_get_declaring_type(Pointer);
                if (pointer != IntPtr.Zero)
                    return new IEClass(pointer);

                return null;
            }
        }

        public IEClass BaseType
        {
            get
            {
                IntPtr pointer = Import.Class.il2cpp_class_get_parent(Pointer);
                if (pointer != IntPtr.Zero)
                    return new IEClass(pointer);

                return null;
            }
        }

        public bool HasAttribute(IEClass klass)
        {
            if (klass == null) return false;
            return Import.Class.il2cpp_class_has_attribute(Pointer, klass.Pointer);
        }

        public bool IsEnum => Import.Class.il2cpp_class_is_enum(Pointer);
        public bool IsAbstract => HasFlag(IL2BindingFlags.TYPE_ABSTRACT);
        public bool IsInterface => HasFlag(IL2BindingFlags.TYPE_INTERFACE);

        public IL2BindingFlags Flags
        {
            get
            {
                uint f = 0;
                return (IL2BindingFlags)Import.Class.il2cpp_class_get_flags(Pointer, ref f);
            }
        }
        public bool HasFlag(IL2BindingFlags flag) => ((Flags & flag) != 0);

        // Methods
        public IE2Method GetMethodByName(string name, int argsCount)
        {
            IntPtr result = Import.Class.il2cpp_class_get_method_from_name(Pointer, new IE2String_utf16(name).Pointer, argsCount);
            if (result != IntPtr.Zero)
                return new IE2Method(result);
            return null;
        }
        public IE2Method[] GetMethods() => MethodList.ToArray();
        public IE2Method[] GetMethods(IL2BindingFlags flags) => GetMethods(flags, null);
        public IE2Method[] GetMethods(Func<IE2Method, bool> func) => GetMethods().Where(x => func(x)).ToArray();
        public IE2Method[] GetMethods(IL2BindingFlags flags, Func<IE2Method, bool> func) => GetMethods().Where(x => x.HasFlag(flags) && func(x)).ToArray();
        public IE2Method GetMethod(Func<IE2Method, bool> func) => GetMethods().Where(x => func(x)).FirstOrDefault();
        public IE2Method GetMethod(string name) => GetMethod(name, null);
        public IE2Method GetMethod(string name, IL2BindingFlags flags) => GetMethod(name, flags, null);
        public IE2Method GetMethod(string name, Func<IE2Method, bool> func)
        {
            IE2Method returnval = null;
            foreach (IE2Method method in GetMethods())
            {
                if (method.Name.Equals(name) && ((func == null) || func(method)))
                {
                    returnval = method;
                    break;
                }
            }
            return returnval;
        }
        public IE2Method GetMethod(string name, IL2BindingFlags flags, Func<IE2Method, bool> func)
        {
            IE2Method returnval = null;
            foreach (IE2Method method in GetMethods())
            {
                if (method.Name.Equals(name) && method.HasFlag(flags) && ((func == null) || func(method)))
                {
                    returnval = method;
                    break;
                }
            }
            return returnval;
        }
        public IE2Method GetMethod(IEClass type)
        {
            IE2Method[] methods = GetMethods();
            int length = methods.Length;
            for (int i = 0; i < length; i++)
            {
                if (methods[i].ReturnType.Name == type.FullName)
                    return methods[i];
            }
            return null;
        }


        // Fields
        public IE2Field[] GetFields() => FieldList.ToArray();
        public IE2Field[] GetFields(IL2BindingFlags flags) => GetFields(flags, null);
        public IE2Field[] GetFields(Func<IE2Field, bool> func) => GetFields().Where(x => func(x)).ToArray();
        public IE2Field[] GetFields(IL2BindingFlags flags, Func<IE2Field, bool> func) => GetFields().Where(x => (x.HasFlag(flags) && func(x))).ToArray();
        public IE2Field GetField(Func<IE2Field, bool> func) => GetFields().FirstOrDefault(x => func(x));
        public IE2Field GetField(string name) => GetField(name, null);
        public IE2Field GetField(string name, IL2BindingFlags flags) => GetField(name, flags, null);
        public IE2Field GetField(IEClass type)
        {
            IE2Field[] fields = GetFields();
            for (int i=0;i< fields.Length;i++)
            {
                if (fields[i].ReturnType.Name == type.FullName)
                    return fields[i];
            }
            return null;
        }
        public IE2Field GetField(string name, Func<IE2Field, bool> func)
        {
            IE2Field returnval = null;
            foreach (IE2Field field in GetFields())
            {
                if (field.Name.Equals(name) && ((func == null) || func(field)))
                {
                    returnval = field;
                    break;
                }
            }
            return returnval;
        }
        public IE2Field GetField(string name, IL2BindingFlags flags, Func<IE2Field, bool> func)
        {
            IE2Field returnval = null;
            foreach (IE2Field field in GetFields())
            {
                if (field.Name.Equals(name) && field.HasFlag(flags) && ((func == null) || func(field)))
                {
                    returnval = field;
                    break;
                }
            }
            return returnval;
        }

        /*
        // Events
        public IL2CPP_Event[] GetEvents() => EventList.ToArray();
        */

        // Properties
        public IE2Property[] GetProperties() => PropertyList.ToArray();
        public IE2Property[] GetProperties(IL2BindingFlags flags) => GetProperties(x => x.HasFlag(flags));
        public IE2Property[] GetProperties(Func<IE2Property, bool> func) => GetProperties().Where(x => func(x)).ToArray();
        public IE2Property GetProperty(Func<IE2Property, bool> func) => GetProperties().FirstOrDefault(x => func(x));
        public IE2Property GetProperty(IEClass type) => GetProperties().FirstOrDefault(x => x.GetGetMethod()?.ReturnType.Name == type.FullName || x.GetSetMethod()?.GetParameters()[0]?.Name == type.FullName);
        public IE2Property GetProperty(string name)
        {
            IE2Property returnval = null;
            foreach (IE2Property prop in GetProperties())
            {
                if (prop.Name.Equals(name))
                {
                    returnval = prop;
                    break;
                }
            }
            return returnval;
        }

        public IE2Property GetProperty(string name, IL2BindingFlags flags)
        {
            IE2Property returnval = null;
            foreach (IE2Property prop in GetProperties())
            {
                if (prop.Name.Equals(name) && prop.HasFlag(flags))
                {
                    returnval = prop;
                    break;
                }
            }
            return returnval;
        }

        // Nested Types
        public IEClass[] GetNestedTypes() => NestedTypeList.ToArray();
        public IEClass[] GetNestedTypes(IL2BindingFlags flags) => GetNestedTypes().Where(x => x.HasFlag(flags)).ToArray();
        public IEClass GetNestedType(string name) => GetNestedType(name, null);
        public IEClass GetNestedType(string name, IL2BindingFlags flags) => GetNestedType(name, null, flags);
        public IEClass GetNestedType(string name, string name_space)
        {
            IEClass returnval = null;
            foreach (IEClass type in GetNestedTypes())
            {
                if (type.Name.Equals(name) && (string.IsNullOrEmpty(type.Namespace) || type.Namespace.Equals(name_space)))
                {
                    returnval = type;
                    break;
                }
            }
            return returnval;
        }
        public IEClass GetNestedType(string name, string name_space, IL2BindingFlags flags)
        {
            IEClass returnval = null;
            foreach (IEClass type in GetNestedTypes())
            {
                if (type.Name.Equals(name) && (string.IsNullOrEmpty(type.Namespace) || type.Namespace.Equals(name_space)) && type.HasFlag(flags))
                {
                    returnval = type;
                    break;
                }
            }
            return returnval;
        }

        // Interface Types
        public IEClass[] GetInterfaceTypes() => InterfaceTypeList.ToArray();
        public IEClass[] GetInterfaceTypes(IL2BindingFlags flags) => GetInterfaceTypes().Where(x => x.HasFlag(flags)).ToArray();
        public IEClass GetInterfaceType(string name) => GetInterfaceType(name, null);
        public IEClass GetInterfaceType(string name, IL2BindingFlags flags) => GetInterfaceType(name, null, flags);
        public IEClass GetInterfaceType(string name, string name_space)
        {
            IEClass returnval = null;
            foreach (IEClass type in GetNestedTypes())
            {
                if (type.Name.Equals(name) && (string.IsNullOrEmpty(type.Namespace) || type.Namespace.Equals(name_space)))
                {
                    returnval = type;
                    break;
                }
            }
            return returnval;
        }
        public IEClass GetInterfaceType(string name, string name_space, IL2BindingFlags flags)
        {
            IEClass returnval = null;
            foreach (IEClass type in GetInterfaceTypes())
            {
                if (type.Name.Equals(name) && (string.IsNullOrEmpty(type.Namespace) || type.Namespace.Equals(name_space)) && type.HasFlag(flags))
                {
                    returnval = type;
                    break;
                }
            }
            return returnval;
        }
    }
}
