using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace JMT.Editor
{
    [InitializeOnLoad]
    public class HierarchySceneMover
    {
        private static bool _isDropdownOpen = false;
        private static HashSet<string> _favoriteScenes = new HashSet<string>();
        private static List<string> _allScenes = new List<string>();
        private static Vector2 _scrollPosition;
        private static string _searchQuery = ""; // 검색 쿼리 추가

        static HierarchySceneMover()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += BlockHierarchySelection;
            LoadScenes();
        }

        private static void LoadScenes()
        {
            _allScenes = AssetDatabase.FindAssets("t:Scene")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(System.IO.Path.GetFileNameWithoutExtension)
                .ToList();
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            if (instanceID != SceneManager.GetActiveScene().handle) return;

            Rect dropdownRect = new Rect(selectionRect.xMax - 20, selectionRect.y, 16, 16);
            if (GUI.Button(dropdownRect, "\u25BC"))
            {
                _isDropdownOpen = !_isDropdownOpen;
            }

            if (_isDropdownOpen)
            {
                DrawSceneDropdown();
            }
        }

        private static void DrawSceneDropdown()
        {
            float dropdownHeight = Mathf.Min(200, _allScenes.Count * 20 + 10);
            Rect dropdownArea = new Rect(0, 0, EditorGUIUtility.currentViewWidth, dropdownHeight + 10);
            EditorGUI.DrawRect(dropdownArea, new Color(0.15f, 0.15f, 0.15f, 1f));

            // 검색창과 X 버튼이 동일한 행에 배치되도록 수정
            Rect searchFieldRect = new Rect(dropdownArea.width - 150, 5, 140, 20); // 검색창 위치
            _searchQuery = GUI.TextField(searchFieldRect, _searchQuery, 20); // 검색 텍스트 필드

            Rect closeButtonRect = new Rect(dropdownArea.width - 35, 5, 20, 20); // X 버튼 위치
            if (GUI.Button(closeButtonRect, "X"))
            {
                _isDropdownOpen = false; // 드롭다운을 닫음
            }

            Rect scrollViewRect = new Rect(5, 30, dropdownArea.width - 10, dropdownHeight);
            Rect contentRect = new Rect(0, 0, scrollViewRect.width - 20, _allScenes.Count * 22 + 10);

            _scrollPosition = GUI.BeginScrollView(scrollViewRect, _scrollPosition, contentRect);

            float yOffset = 30; // 'Close' 버튼 아래로 씬 목록을 그리기 위해 yOffset 조정
            bool hasFavorites = _favoriteScenes.Count > 0;

            // 검색 쿼리로 씬 필터링
            IEnumerable<string> filteredScenes = _allScenes.Where(scene => scene.ToLower().Contains(_searchQuery.ToLower()));

            foreach (var scene in filteredScenes.OrderByDescending(s => _favoriteScenes.Contains(s)))
            {
                if (hasFavorites && !_favoriteScenes.Contains(scene))
                {
                    Handles.DrawLine(new Vector3(5, yOffset), new Vector3(contentRect.width - 5, yOffset));
                    hasFavorites = false;
                    yOffset += 2;
                }

                Rect sceneRect = new Rect(10, yOffset, contentRect.width - 50, 20);
                if (GUI.Button(sceneRect, scene, new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft, normal = { textColor = Color.white }, fontSize = 12, border = new RectOffset(0, 0, 0, 0), margin = new RectOffset(0, 0, 0, 0), padding = new RectOffset(4, 4, 2, 2) }))
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(AssetDatabase.FindAssets("t:Scene").Select(AssetDatabase.GUIDToAssetPath).FirstOrDefault(p => System.IO.Path.GetFileNameWithoutExtension(p) == scene));
                }

                Rect favoriteRect = new Rect(contentRect.width - 35, yOffset, 20, 20);
                if (GUI.Button(favoriteRect, _favoriteScenes.Contains(scene) ? "★" : "☆"))
                {
                    ToggleFavorite(scene);
                }

                yOffset += 22;
            }

            GUI.EndScrollView();
        }

        private static void BlockHierarchySelection(int instanceID, Rect selectionRect)
        {
            if (_isDropdownOpen && Event.current.type != EventType.Layout) // Layout 이벤트 타입 제외
            {
                Event.current.Use();
            }
        }

        private static void ToggleFavorite(string scene)
        {
            if (_favoriteScenes.Contains(scene))
            {
                _favoriteScenes.Remove(scene);
            }
            else
            {
                _favoriteScenes.Add(scene);
            }
        }
    }
}
