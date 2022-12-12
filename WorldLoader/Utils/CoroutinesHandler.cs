using Il2CppGen.Runtime.Injection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldLoader.Utils
{
    public static class CoroutinesHandler
    {
        internal static List<IEnumerator> BackLog = new();

        public static void Start(this IEnumerator enumerator) {
            BackLog.Add(enumerator);
        }
    }


    internal class MonoEnumeratorWrapper : Il2CppSystem.Object //, IEnumerator
    {
        private readonly IEnumerator enumerator;
        public MonoEnumeratorWrapper(IntPtr ptr) : base(ptr) { }
        public MonoEnumeratorWrapper(IEnumerator _enumerator) : base(ClassInjector.DerivedConstructorPointer<MonoEnumeratorWrapper>())
        {
            ClassInjector.DerivedConstructorBody(this);
            enumerator = _enumerator ?? throw new NullReferenceException("routine is null");
        }

        public Il2CppSystem.Object /*IEnumerator.*/Current
        {
            get => enumerator.Current switch
            {
                IEnumerator next => new MonoEnumeratorWrapper(next),
                Il2CppSystem.Object il2cppObject => il2cppObject,
                null => null,
                _ => throw new NotSupportedException($"{enumerator.GetType()}: Unsupported type {enumerator.Current.GetType()}"),
            };
        }

        public bool MoveNext() => enumerator.MoveNext();
        public void Reset() => enumerator.Reset();
    }
}
