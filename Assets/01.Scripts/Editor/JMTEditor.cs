using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

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
            string[] guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/00.Scenes" });
            _scenes = new string[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                _scenes[i] = path.Substring(path.LastIndexOf('/') + 1);
            }
            
            // Build Settings에 포함된 씬들 가져오기
            scenes = EditorBuildSettings.scenes;
        
            // ReorderableList 생성
            reorderableList = new ReorderableList(scenes, typeof(EditorBuildSettingsScene), true, true, true, true)
            {
                drawElementCallback = DrawSceneListElement,
                onReorderCallback = OnReorderCallback,
                onAddCallback = OnAddSceneCallback,
                onRemoveCallback = OnRemoveSceneCallback
            };
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("JMT Manager", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Build");
            SetBuildType();
            _isDevelopmentBuild = EditorGUILayout.Toggle("Development", _isDevelopmentBuild);
            if (GUILayout.Button("Build"))
            {
                Build();
            }
            SetRemoteSettings();
            
            
            EditorGUILayout.LabelField("Tools");
            ChangeScenes();
            SceneList();
            reorderableList.DoLayoutList();
        }

        private void SceneList()
        {
            
        }

        private void ChangeScenes()
        {
            selectedSceneIndex = EditorGUILayout.Popup("Select Scene", selectedSceneIndex, _scenes);

            if (GUILayout.Button("Load Scene"))
            {
                LoadScene(_scenes[selectedSceneIndex]);
            }
        }

        private void LoadScene(string scene)
        {
            string scenePath = "00.Scenes/" + scene;
            string fullScenePath = "Assets/" + scenePath;

            if (!IsSceneInBuildSettings(fullScenePath))
            {
                AddSceneToBuildSettings(fullScenePath);
            }

            EditorSceneManager.OpenScene(fullScenePath);
            LogFormat($"Scene Load: {scene}");
        }
        
        private bool IsSceneInBuildSettings(string scenePath)
        {
            var scenes = EditorBuildSettings.scenes;
            foreach (var scene in scenes)
            {
                if (scene.path == scenePath)
                    return true;
            }
            return false;
        }

        private void AddSceneToBuildSettings(string scenePath)
        {
            var newScene = new EditorBuildSettingsScene(scenePath, true);
            var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            scenes.Add(newScene);
            EditorBuildSettings.scenes = scenes.ToArray();
        }

        private void SetRemoteSettings()
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("Remote Settings");
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Android"))
                    {
                        EditorSettings.unityRemoteDevice = "Any Android Device";  
                        LogFormat($"Unity Remote Device 설정 변경: {EditorSettings.unityRemoteDevice}");
                    }
                    if (GUILayout.Button("None"))
                    {
                        EditorSettings.unityRemoteDevice = "None";
                        LogFormat($"Unity Remote Device 설정 변경: {EditorSettings.unityRemoteDevice}");
                    }
                    
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        private void Build()
        {
            string buildPath = "Builds/" + EditorUserBuildSettings.activeBuildTarget;
            if (!Directory.Exists(buildPath))
            {
                Directory.CreateDirectory(buildPath);
            }

            LogFormat("Build Start");

            var scenes = EditorBuildSettings.scenes;
            string[] scenePaths = new string[scenes.Length];
            for (int i = 0; i < scenes.Length; i++)
            {
                scenePaths[i] = scenes[i].path;
            }

            BuildPlayerOptions buildPlayerOptions = new()
            {
                scenes = scenePaths,
                locationPathName = Path.Combine(buildPath, "GameBuild" + GetBuildExtension()),
                target = EditorUserBuildSettings.activeBuildTarget,
                options = _isDevelopmentBuild ? BuildOptions.Development : BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                LogFormat("Build Success! Size: {0} bytes", summary.totalSize);
            }
            else
            {
                LogFormat("Build Failed!");
            }
        }

        private string GetBuildExtension()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneWindows: return ".exe";
                case BuildTarget.StandaloneWindows64: return ".exe";
                case BuildTarget.StandaloneOSX: return ".app";
                case BuildTarget.Android: return ".apk";
                case BuildTarget.WebGL: return "";
                default: return "";
            }
        }

        private void LogFormat(string format, params object[] args)
        {
            Debug.LogFormat(format, args);
        }

        private void SetBuildType()
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("Build Type");
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Android"))
                    {
                        SetBuildTarget(BuildType.Android);
                        LogFormat("Build Target 변경: Android");
                    }

                    if (GUILayout.Button("Windows"))
                    {
                        SetBuildTarget(BuildType.Windows);
                        LogFormat("Build Target 변경: Windows");
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        private void SetBuildTarget(BuildType buildTarget)
        {
            if (buildTarget == BuildType.Android)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            }
            else if (buildTarget == BuildType.Windows)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
            }
        }
        
        private void DrawSceneListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            // scenes 배열의 크기보다 인덱스가 커지지 않도록 확인
            if (index >= scenes.Length) return;

            EditorGUI.LabelField(rect, scenes[index].path);
        }

        
        private void OnReorderCallback(ReorderableList list)
        {
            // 드래그 앤 드롭으로 순서가 변경되었을 때 처리
            UpdateBuildSettingsScenes();
        }
        
        private void OnAddSceneCallback(ReorderableList list)
        {
            // 씬을 추가하려면 씬 파일을 선택하여 추가할 수 있도록 해야 합니다.
            string scenePath = EditorUtility.OpenFilePanel("Select Scene to Add", "Assets", "unity");
    
            if (!string.IsNullOrEmpty(scenePath))
            {
                // AssetDatabase.GetAssetPath를 사용하여 상대 경로로 변환
                string relativeScenePath = FileUtil.GetProjectRelativePath(scenePath);

                // 씬이 상대 경로로 변환되었는지 확인하고 추가
                if (!string.IsNullOrEmpty(relativeScenePath))
                {
                    var newScene = new EditorBuildSettingsScene(relativeScenePath, true);
                    var scenesList = new List<EditorBuildSettingsScene>(scenes);
                    scenesList.Add(newScene);
                    scenes = scenesList.ToArray();
                    UpdateBuildSettingsScenes();
                }
                else
                {
                    Debug.LogError("Invalid scene path.");
                }
            }
        }

        
        private void OnRemoveSceneCallback(ReorderableList list)
        {
            // 씬을 삭제하면 Build Settings에서 해당 씬을 제거합니다.
            if (list.index >= 0 && list.index < scenes.Length)
            {
                var scenesList = new System.Collections.Generic.List<EditorBuildSettingsScene>(scenes);
                scenesList.RemoveAt(list.index);
                scenes = scenesList.ToArray();
                UpdateBuildSettingsScenes();
            }
        }

        private void UpdateBuildSettingsScenes()
        {
            // 수정된 씬 배열을 Build Settings에 업데이트합니다.
            EditorBuildSettings.scenes = scenes;
        }

        private void LogFormat(string message)
        {
            Debug.Log($"[JMTManager] {message}");
        }
    }

    [InitializeOnLoad]
    static class AutoSaveOnExit
    {
        static AutoSaveOnExit()
        {
            EditorApplication.quitting += SaveOnExit;
        }

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