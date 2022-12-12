using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WorldLoader.Mods;
using WorldLoader.Utils;

namespace WorldLoader
{
    internal class MonoBehv : MonoBehaviour
    {
        public MonoBehv(IntPtr id) : base(id)
        {
        }

        void Update()
        {
            foreach (UnityMod vrMod in WorldLoader._ModManager.Mods) Internal_Utils.RunInTry(vrMod.OnUpdate, $"Error During Update on {vrMod.Name}\n");
            var bakclog = CoroutinesHandler.BackLog;
            foreach (var Emn in bakclog) {
                var Wrapper = new MonoEnumeratorWrapper(Emn);
                StartCoroutine(new Il2CppSystem.Collections.IEnumerator(Wrapper.Pointer));
                //CoroutinesHandler.BackLog.Remove(Emn);
            }
        }

        void OnGUI()
        {
            foreach (UnityMod vrMod in WorldLoader._ModManager.Mods) Internal_Utils.RunInTry(vrMod.OnGui, $"Error During OnGUI on {vrMod.Name}\n");
        }
    }
}
