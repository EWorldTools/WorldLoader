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

        void Awake()
        {
            foreach (var Emn in CoroutinesHandler.BackLog) StartCoroutine(new Il2CppSystem.Collections.IEnumerator(new MonoEnumeratorWrapper(Emn).Pointer));

        }

        internal void StartEmn(IEnumerator enumerator) =>
            StartCoroutine(new Il2CppSystem.Collections.IEnumerator(new MonoEnumeratorWrapper(enumerator).Pointer));


        void Update()
        {
            foreach (UnityMod vrMod in WorldLoader._ModManager.Mods) Internal_Utils.RunInTry(vrMod.OnUpdate, $"Error During Update on {vrMod.Name}\n");
        }

        void OnGUI()
        {
            foreach (UnityMod vrMod in WorldLoader._ModManager.Mods) Internal_Utils.RunInTry(vrMod.OnGui, $"Error During OnGUI on {vrMod.Name}\n");
        }
    }
}
