using Il2CppGen.Runtime.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WorldLoader.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterTypeInIl2Cpp : Attribute {
        internal static List<Assembly> registrationQueue = new List<Assembly>();
        internal static bool ready;

        public RegisterTypeInIl2Cpp() { }

        public static void RegisterAssembly(Assembly asm)
        {

            if (!ready)
            {
                registrationQueue.Add(asm);
                return;
            }

            IEnumerable<Type> typeTbl = asm.GetValidTypes();
            if ((typeTbl == null) || (typeTbl.Count() <= 0))
                return;
            foreach (Type type in typeTbl)
            {
                object[] attTbl = type.GetCustomAttributes(typeof(RegisterTypeInIl2Cpp), false);
                if ((attTbl == null) || (attTbl.Length <= 0))
                    continue;
                RegisterTypeInIl2Cpp att = (RegisterTypeInIl2Cpp)attTbl[0];
                if (att == null)
                    continue;
                RegisterTypeInIl2CppDomain(type);
            }
        }

        public static void RegisterTypeInIl2CppDomain(Type type)
            => ClassInjector.RegisterTypeInIl2Cpp(type);

        internal static void SetReady()
        {
            ready = true;

            if (registrationQueue == null)
                return;

            foreach (var asm in registrationQueue)
                RegisterAssembly(asm);

            registrationQueue = null;
        }
    }
    internal static class InjectUtils
    {
        public static IEnumerable<Type> GetValidTypes(this Assembly asm)
        {
            IEnumerable<Type> returnval = Enumerable.Empty<Type>();
            try { returnval = asm.GetTypes().AsEnumerable(); }
            catch (ReflectionTypeLoadException ex) { returnval = ex.Types; }

            return returnval.Where(x => (x != null));
        }

    }
}
