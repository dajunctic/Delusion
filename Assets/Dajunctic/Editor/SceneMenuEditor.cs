#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dajunctic
{
    public static class SceneMenu
    {
        const string PrefabScene = "PrefabScene";
        const string LauncherScene = "LauncherScene";
        const string HomeScene = "HomeScene";
        const string GameScene = "GameScene";

        static void ChangeScene(string name)
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene(Application.dataPath + "Delusion/Scenes/" + name + ".unity");
        }

        static bool CanChangeScene(string name)
        {
            return HasScene(name) && SceneManager.GetActiveScene().name != name;
        }

        static bool HasScene(string name)
        {
            return File.Exists(Application.dataPath + "Delusion/Scenes/" + name + ".unity");
        }

        [MenuItem("Scenes/Prefab Scene", false, 11)]
        static void OpenPrefabScene()
        {
            ChangeScene(PrefabScene);
        }

        [MenuItem("Scenes/Prefab Scene", true, 11)]
        static bool CanOpenPrefabScene()
        {
            return CanChangeScene(PrefabScene);
        }

        
        [MenuItem("Scenes/Launcher Scene", false, 22)]
        static void OpenLauncherScene()
        {
            ChangeScene(LauncherScene);
        }

        [MenuItem("Scenes/Launcher Scene", true, 22)]
        static bool CanOpenLauncherScene()
        {
            return CanChangeScene(LauncherScene);
        }
        
        [MenuItem("Scenes/Home Scene", false, 22)]
        static void OpenHomeScene()
        {
            ChangeScene(HomeScene);
        }

        [MenuItem("Scenes/Home Scene", true, 22)]
        static bool CanOpenHomeScene()
        {
            return CanChangeScene(HomeScene);
        }

           
        [MenuItem("Scenes/Game Scene", false, 22)]
        static void OpenGameScene()
        {
            ChangeScene(GameScene);
        }

        [MenuItem("Scenes/Game Scene", true, 22)]
        static bool CanOpenGameScene()
        {
            return CanChangeScene(GameScene);
        }

        [MenuItem("Scenes/Play", false, 44)]
        public static void Play()
        {
            if (HasScene(LauncherScene))
            {
                EditorSceneManager.SaveOpenScenes();
                ChangeScene(LauncherScene);
                EditorApplication.isPlaying = true;
            }
        }

        [MenuItem("Scenes/Play", true, 44)]
        static bool CanPlay()
        {
            return HasScene(LauncherScene) && !Application.isPlaying;
        }
    }
}
#endif
