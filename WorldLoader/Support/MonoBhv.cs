//using WorldLoader.Il2CppGen.Runtime;
//using WorldLoader.Il2CppGen.Runtime.Injection;
//using System;
//using UnityEngine;
//using WorldLoader.HookUtils;

//namespace WorldLoader.Support
//{
//    internal class MonoBhv
//    {
//        internal static GameObject Obj;
//        internal static UpdateComponet component = null;

//        internal static void Start()
//        {
//            ClassInjector.RegisterTypeInIl2Cpp<UpdateComponet>();
//            new GameObject().AddComponent<UpdateComponet>();
//            //UpdateComponet.Create();
//        }
//    }

//    internal class UpdateComponet : MonoBehaviour
//    {
//        public UpdateComponet(IntPtr obj0) : base(obj0)
//        {
//        }

//        private bool isQuitting;

//        internal static void Create()
//        {
//            MonoBhv.Obj = new GameObject();
//            DontDestroyOnLoad(MonoBhv.Obj);
//            MonoBhv.Obj.hideFlags = HideFlags.DontSave;
//            MonoBhv.component = MonoBhv.Obj.AddComponent(Il2CppType.Of<UpdateComponet>()).TryCast<UpdateComponet>();
//            MonoBhv.component.LastSibling();
//        }

//        private void LastSibling()
//        {
//            gameObject.transform.SetAsLastSibling();
//            transform.SetAsLastSibling();
//        }

//        internal void Destroy() =>
//            Destroy(gameObject);
        

//        void Start()=>
//            LastSibling();
        

//        void Awake()
//        {
//            //foreach (var queuedCoroutine in SupportModule_To.QueuedCoroutines)
//            //    StartCoroutine(new Il2CppSystem.Collections.IEnumerator(new MonoEnumeratorWra(queuedCoroutine).Pointer));
//        }

//        void Update()
//        {
//            isQuitting = false;

//            LastSibling();
//           foreach (var c in WorldLoader._ModManager.Mods)
//                try {
//                    c.OnUpdate();
//                } catch (Exception e) {
//                    Logs.Error($"Error During OnSceneUnload for {c.Name}", e);
//                } 
//        }

//        void OnDestroy()
//        {
//            if (!isQuitting) {
//                Create();
//                return;
//            }
//        }

//        void OnApplicationQuit()
//        {
//            isQuitting = true;
//        }

//        //void FixedUpdate() => MonoBhv.Interface.FixedUpdate();
//        //void LateUpdate() => MonoBhv.Interface.LateUpdate();

//        void OnGUI()
//        {
//           foreach (var c in WorldLoader._ModManager.Mods)
//                try {
//                    c.OnGui();
//                } catch (Exception e) {
//                    Logs.Error($"Error During OnSceneUnload for {c.Name}", e);
//                } 
//        }
//    }
//}
