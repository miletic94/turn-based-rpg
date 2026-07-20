using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CoreDomain.Scripts.Editor.DefaultSceneSelector
{
    [InitializeOnLoad]
    public static class DefaultSceneSelector
    {
        private const string DEFAULT_SCENE_PATH_KEY = "DefaultSceneKey";
        private const string HAS_OPENED_PROJECT_BEFORE_KEY = "HasOpenedProjectBeforeKey";
        private const string BOOT_SCENE_PATH = "Assets/Core/Assets/Scenes/BootScene.unity";
        private const string GAMEPLAY_SCENE_PATH = "Assets/Core/Game/GamePlay/Assets/Scenes/GameplayScene.unity";

        static DefaultSceneSelector()
        {
            EditorApplication.delayCall += OnLoad;
        }

        private static void OnLoad()
        {
            OpenCoreSceneIfHaventBefore();
            SetSavedSceneAsStarting();
        }

        private static void SetSavedSceneAsStarting()
        {
            var path = EditorPrefs.GetString(DEFAULT_SCENE_PATH_KEY, BOOT_SCENE_PATH);
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            EditorSceneManager.playModeStartScene = sceneAsset;
        }

        private static void OpenCoreSceneIfHaventBefore()
        {
            var didOpenProjectBefore = EditorPrefs.HasKey(HAS_OPENED_PROJECT_BEFORE_KEY);
            if (didOpenProjectBefore)
            {
                return;
            }
            EditorPrefs.SetBool(HAS_OPENED_PROJECT_BEFORE_KEY, true);
        }

        [MenuItem("Tools/ElephantKarter/Scene/Select Default Scene", false, 1)]
        private static void SelectDefaultScene()
        {
            var absolutePath = EditorUtility.OpenFilePanel("Select default scene", GetSelectedFolder(), "unity");
            if (string.IsNullOrEmpty(absolutePath))
            {
                return;
            }
            var path = GetProjectRelativePath(absolutePath);
            EditorPrefs.SetString(DEFAULT_SCENE_PATH_KEY, path);
            SetSavedSceneAsStarting();
        }

        private static string GetSelectedFolder()
        {
            var obj = Selection.activeObject;
            return obj == null ? "Assets" : AssetDatabase.GetAssetPath(obj.GetEntityId()); ;
        }

        private static string GetProjectRelativePath(string absolutePath)
        {
            if (absolutePath.StartsWith(Application.dataPath))
            {
                return "Assets" + absolutePath.Substring(Application.dataPath.Length);
            }

            Debug.LogError("Selected file is not within the project's Assets folder.");
            return null;
        }

        [MenuItem("Tools/ElephantKarter/Scene/Reset Default Scene", false, 2)]
        private static void ResetDefaultScene()
        {
            EditorPrefs.DeleteKey(DEFAULT_SCENE_PATH_KEY);
            EditorSceneManager.playModeStartScene = null;
        }

        [MenuItem("Tools/ElephantKarter/Scene/Open/Boot Scene &2", false, 3)]
        private static void OpenBootScene()
        {
            EditorApplication.ExitPlaymode();
            EditorSceneManager.OpenScene(BOOT_SCENE_PATH);
        }

        [MenuItem("Tools/ElephantKarter/Scene/Open/Gameplay Scene &3", false, 4)]
        private static void OpenGameplayScene()
        {
            EditorApplication.ExitPlaymode();
            EditorSceneManager.OpenScene(GAMEPLAY_SCENE_PATH);
        }
    }
}