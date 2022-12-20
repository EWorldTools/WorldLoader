using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using WorldLoader;
using WorldLoader.HookUtils;

namespace Runtime.Il2cpp
{
    internal class SceneHook // ML
    {
        private static void OnSceneUnload(Scene scene) { if (scene == null) return; 
            foreach (var c in WorldLoader.WorldLoader._ModManager.Mods)
                try {
                    c.OnSceneWasUnloaded(scene.buildIndex, scene.name);
                } catch (Exception e) {
                    Logs.Error($"Error During OnSceneUnload for {c.Name}", e);
                } 
        }

        private static void OnSceneLoad(Scene scene, LoadSceneMode mode) { if (scene == null) return; 
            foreach (var c in WorldLoader.WorldLoader._ModManager.Mods)
                try {
                    c.OnSceneWasLoaded(scene.buildIndex, scene.name);
                } catch (Exception e) {
                    Logs.Error($"Error During OnSceneUnload for {c.Name}", e);
                } 
        }

        public static void SceneManagementInit()
        {
            try
            {
                SceneManager.sceneLoaded = (
                    (ReferenceEquals(SceneManager.sceneLoaded, null))
                    ? new Action<Scene, LoadSceneMode>(OnSceneLoad)
                    : Il2CppSystem.Delegate.Combine(SceneManager.sceneLoaded, (UnityAction<Scene, LoadSceneMode>)new Action<Scene, LoadSceneMode>(OnSceneLoad)).Cast<UnityAction<Scene, LoadSceneMode>>()
                    );
            }
            catch (Exception ex) { Logs.Error($"SceneManager.sceneLoaded override failed: {ex}"); }


            try
            {
                SceneManager.sceneUnloaded = (
                    (ReferenceEquals(SceneManager.sceneUnloaded, null))
                    ? new Action<Scene>(OnSceneUnload)
                    : Il2CppSystem.Delegate.Combine(SceneManager.sceneUnloaded, (UnityAction<Scene>)new Action<Scene>(OnSceneUnload)).Cast<UnityAction<Scene>>()
                    );
            }
            catch (Exception ex) { Logs.Error($"SceneManager.sceneUnloaded override failed: {ex}"); }
        }
    }
}
