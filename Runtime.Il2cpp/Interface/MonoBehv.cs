using Il2CppGen.Runtime.Injection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WorldLoader.HookUtils;
using WorldLoader.Mods;
using WorldLoader.Utils;

namespace WorldLoader
{
    public class MonoBehv : MonoBehaviour
    {
        public MonoBehv(IntPtr id) : base(id)
        {
        }

        public static void Prep() {
            if (!ClassInjector.IsTypeRegisteredInIl2Cpp(typeof(MonoBehv)))
                ClassInjector.RegisterTypeInIl2Cpp<MonoBehv>();
            var obj = new GameObject("TestOBJ").AddComponent<MonoBehv>();
            UnityEngine.Object.DontDestroyOnLoad(obj);
        }

        void Update()
        {
            foreach (UnityMod vrMod in WorldLoader._ModManager.Mods) ModUtils.RunInTry(vrMod.OnUpdate, $"Error During Update on {vrMod.Name}\n");
            foreach (var Emn in CoroutinesHandler.BackLog) {
                var Wrapper = new MonoEnumeratorWrapper(Emn);
                StartCoroutine(new Il2CppSystem.Collections.IEnumerator(Wrapper.Pointer));
            }
            CoroutinesHandler.BackLog.Clear();
        }

        void OnGUI()
        {
            foreach (UnityMod vrMod in WorldLoader._ModManager.Mods) ModUtils.RunInTry(vrMod.OnGui, $"Error During OnGUI on {vrMod.Name}\n");
        }
    }
}
