using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace JMT.Editor
{
    public class JMTEditor : EditorWindow
    {
        private bool _isDevelopmentBuild;
        private string[] _scenes;
        private int selectedSceneIndex = 0;
        private ReorderableList reorderableList;
        private EditorBuildSettingsScene[] scenes;

        [MenuItem("JMT/Manager")]
        private static void OpenWindow()
        {
            JMTEditor window = GetWindow<JMTEditor>("JMT Manager");
            window.Show();
        }

        private void OnEnable()
        {
            LoadScenes();
            SetupReorderableList();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("JMT Manager", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            DrawBuildSettings();
            DrawSceneManagement();
        }
 
        private void LoadScenes()
        {
            string[] guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/00.Scenes" });
            _scenes = Array.ConvertAll(guids, AssetDatabase.GUIDToAssetPath);
            scenes = EditorBuildSettings.scenes;
        }

        private void SetupReorderableList()
        {
            reorderableList = new ReorderableList(scenes, typeof(EditorBuildSettingsScene), true, true, true, true)
            {
                drawElementCallback = DrawSceneListElement,
                onReorderCallback = _ => UpdateBuildSettingsScenes(),
                onAddCallback = OnAddSceneCallback,
                onRemoveCallback = OnRemoveSceneCallback
            };
        }

        private void DrawBuildSettings()
        {
            EditorGUILayout.LabelField("Build Settings", EditorStyles.boldLabel);
            _isDevelopmentBuild = EditorGUILayout.Toggle("Development", _isDevelopmentBuild);
            DrawBuildTargetButtons();
            if (GUILayout.Button("Build")) Build();
            EditorGUILayout.Space();
        }

        private void DrawBuildTargetButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Android")) SetBuildTarget(BuildType.Android);
            if (GUILayout.Button("Windows")) SetBuildTarget(BuildType.Windows);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSceneManagement()
        {
            EditorGUILayout.LabelField("Scene Management", EditorStyles.boldLabel);
            selectedSceneIndex = EditorGUILayout.Popup("Select Scene", selectedSceneIndex, _scenes);
            if (GUILayout.Button("Load Scene")) LoadScene(_scenes[selectedSceneIndex]);
            reorderableList.DoLayoutList();
        }

        private void LoadScene(string scenePath)
        {
            if (!IsSceneInBuildSettings(scenePath)) AddSceneToBuildSettings(scenePath);
            EditorSceneManager.OpenScene(scenePath);
            Debug.Log($"Scene Loaded: {scenePath}");
        }

        private bool IsSceneInBuildSettings(string scenePath) => Array.Exists(scenes, scene => scene.path == scenePath);

        private void AddSceneToBuildSettings(string scenePath)
        {
            var newScene = new EditorBuildSettingsScene(scenePath, true);
            var sceneList = new List<EditorBuildSettingsScene>(scenes) { newScene };
            EditorBuildSettings.scenes = sceneList.ToArray();
        }

        private void Build()
        {
            string buildPath = Path.Combine("Builds", EditorUserBuildSettings.activeBuildTarget.ToString());
            if (!Directory.Exists(buildPath)) Directory.CreateDirectory(buildPath);

            string[] scenePaths = Array.ConvertAll(EditorBuildSettings.scenes, s => s.path);
            var buildOptions = new BuildPlayerOptions
            {
                scenes = scenePaths,
                locationPathName = Path.Combine(buildPath, "GameBuild" + GetBuildExtension()),
                target = EditorUserBuildSettings.activeBuildTarget,
                options = _isDevelopmentBuild ? BuildOptions.Development : BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(buildOptions);
            Debug.Log(report.summary.result == BuildResult.Succeeded ? "Build Success!" : "Build Failed!");
        }

        private string GetBuildExtension() => EditorUserBuildSettings.activeBuildTarget switch
        {
            BuildTarget.StandaloneWindows => ".exe",
            BuildTarget.StandaloneWindows64 => ".exe",
            BuildTarget.StandaloneOSX => ".app",
            BuildTarget.Android => ".apk",
            _ => ""
        };

        private void SetBuildTarget(BuildType buildTarget)
        {
            BuildTargetGroup group = buildTarget == BuildType.Android ? BuildTargetGroup.Android : BuildTargetGroup.Standalone;
            BuildTarget target = buildTarget == BuildType.Android ? BuildTarget.Android : BuildTarget.StandaloneWindows64;
            EditorUserBuildSettings.SwitchActiveBuildTarget(group, target);
            Debug.Log($"Build Target Set: {target}");
        }

        private void DrawSceneListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (index >= scenes.Length) return;
            EditorGUI.LabelField(rect, scenes[index].path);
        }

        private void OnAddSceneCallback(ReorderableList list)
        {
            string scenePath = EditorUtility.OpenFilePanel("Select Scene to Add", "Assets", "unity");
            if (string.IsNullOrEmpty(scenePath)) return;

            string relativeScenePath = FileUtil.GetProjectRelativePath(scenePath);
            if (!string.IsNullOrEmpty(relativeScenePath))
            {
                var newScene = new EditorBuildSettingsScene(relativeScenePath, true);
                var sceneList = new List<EditorBuildSettingsScene>(scenes) { newScene };
                scenes = sceneList.ToArray();
                UpdateBuildSettingsScenes();
            }
        }

        private void OnRemoveSceneCallback(ReorderableList list)
        {
            if (list.index < 0 || list.index >= scenes.Length) return;
            var sceneList = new List<EditorBuildSettingsScene>(scenes);
            sceneList.RemoveAt(list.index);
            scenes = sceneList.ToArray();
            UpdateBuildSettingsScenes();
        }

        private void UpdateBuildSettingsScenes()
        {
            EditorBuildSettings.scenes = scenes;
        }
    }

    [InitializeOnLoad]
    static class AutoSaveOnExit
    {
        static AutoSaveOnExit() => EditorApplication.quitting += SaveOnExit;
        private static void SaveOnExit()
        {
            AssetDatabase.SaveAssets();
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            EditorApplication.quitting -= SaveOnExit;
        }
    }

    enum BuildType
    {
        Android,
        Windows
    }
}
