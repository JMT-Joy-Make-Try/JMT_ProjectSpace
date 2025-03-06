using UnityEditor;
using UnityEngine;
using JMT.Planets.Tile;
using System.Collections.Generic;
using System.IO;

namespace JMT.Editor
{
    public class TilesSOGenerator : EditorWindow
    {
        private Vector2 scrollPosition;
        private Vector2 scrollPosition2;
        private List<TileData> tileDataList = new List<TileData>();
        private TilesSO selectedTilesSO;

        [MenuItem("JMT/Generate Tiles SO")]
        public static void Open()
        {
            var window = GetWindow<TilesSOGenerator>();
            window.titleContent = new GUIContent("Tiles SO Generator");
            window.minSize = new Vector2(600, 600);
            window.maxSize = new Vector2(600, 600);
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Space(20);
            GUILayout.Label("Generate TilesSO", EditorStyles.boldLabel);

            GUILayout.Space(10);
            GUILayout.Label("Existing TilesSO Assets", EditorStyles.boldLabel);

            string tilesPath = "Assets/07.SO/Tiles/";
            string[] tilesSOPaths = Directory.GetFiles(tilesPath, "*.asset", SearchOption.TopDirectoryOnly);

            // 왼쪽에 버튼을 배치하는 스크롤뷰
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(200)); // 버튼들 왼쪽에 배치
            scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2, true, false);
            foreach (var path in tilesSOPaths)
            {
                string fileName = Path.GetFileName(path);
                if (GUILayout.Button(fileName, GUILayout.Width(150), GUILayout.Height(40)))
                {
                    LoadTilesSO(path);
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            // 오른쪽에 선택된 TilesSO의 정보를 표시하는 영역
            GUILayout.BeginVertical(); // 오른쪽 정보 영역
            if (selectedTilesSO != null)
            {
                GUILayout.Label($"Editing: {selectedTilesSO.name}", EditorStyles.boldLabel);

                // TileData 리스트 UI (세로로 나열)
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);

                for (int i = 0; i < selectedTilesSO.tiles.Count; i++)
                {
                    GUILayout.BeginHorizontal("box");

                    // 타일 타입 선택
                    selectedTilesSO.tiles[i].TileType = (TileType)EditorGUILayout.EnumPopup("Tile Type",
                        selectedTilesSO.tiles[i].TileType, GUILayout.Width(230));
                    GUILayout.Space(10);
                    // 타일 색상 선택
                    selectedTilesSO.tiles[i].Color = EditorGUILayout.ColorField("Color", selectedTilesSO.tiles[i].Color,
                        GUILayout.Width(150));
                    GUILayout.Space(10);

                    // 타일 개수 입력
                    selectedTilesSO.tiles[i].Count = EditorGUILayout.IntField("Count", selectedTilesSO.tiles[i].Count,
                        GUILayout.Width(200));

                    // 타일 삭제 버튼
                    if (GUILayout.Button("Delete", GUILayout.Width(80), GUILayout.Height(20)))
                    {
                        selectedTilesSO.tiles.RemoveAt(i);
                        break; // 리스트 크기가 바뀌므로 인덱스 오류 방지
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.Space(10); // 간격 추가
                }

                GUILayout.EndScrollView();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add Tile", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    selectedTilesSO.tiles.Add(new TileData());
                }

                if (GUILayout.Button("Save TilesSO", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    SaveTilesSO();
                }

                GUILayout.EndHorizontal();

                // TilesSO 삭제 버튼
                GUILayout.Space(20);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Delete TilesSO", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    DeleteTilesSO();
                }

                // SO 삭제 버튼 추가
                if (GUILayout.Button("Delete SO", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    DeleteSO();
                }

                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("No TilesSO selected", EditorStyles.miniLabel);
            }

            GUILayout.EndVertical(); // 오른쪽 정보 영역 끝

            GUILayout.EndHorizontal(); // 버튼과 정보 영역 끝

            GUILayout.Space(20);

            // TilesSO 생성 버튼 (가로로 배치)
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Generate New Tiles SO", GUILayout.Width(200), GUILayout.Height(40)))
            {
                GenerateTilesSO();
            }

            GUILayout.EndHorizontal();
        }

        // 새로운 SO 삭제 함수 추가
        private void DeleteSO()
        {
            string path = "Assets/07.SO/Tiles/TilesSO.asset";
            if (File.Exists(path))
            {
                if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete the TilesSO?", "Yes", "No"))
                {
                    AssetDatabase.DeleteAsset(path);
                    selectedTilesSO = null; // 선택된 TilesSO 초기화
                    Debug.Log("TilesSO deleted successfully!");
                }
            }
            else
            {
                Debug.LogWarning("TilesSO does not exist.");
            }
        }


        // TilesSO 에셋을 생성하고 저장하는 함수
        private void GenerateTilesSO()
        {
            // TilesSO 에셋을 생성
            TilesSO tilesSO = ScriptableObject.CreateInstance<TilesSO>();
            tilesSO.tiles = tileDataList;

            // 프로젝트 경로에 에셋 저장
            string path = "Assets/07.SO/Tiles/TilesSO.asset";
            if (!Directory.Exists(Application.dataPath + "/" + path))
            {
                Directory.CreateDirectory(Application.dataPath + "/" + path);
            }

            if (AssetDatabase.LoadAssetAtPath<TilesSO>(path) != null)
            {
                if (!EditorUtility.DisplayDialog("Warning", "TilesSO already exists. Do you want to overwrite it?",
                        "Yes", "No"))
                {
                    return;
                }
            }

            AssetDatabase.CreateAsset(tilesSO, path);
            AssetDatabase.SaveAssets();

            // 에셋을 인스펙터에서 바로 볼 수 있도록
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = tilesSO;

            Debug.Log("TilesSO Generated and Saved!");
        }

        // TilesSO 에셋을 로드하여 선택하는 함수
        private void LoadTilesSO(string path)
        {
            selectedTilesSO = AssetDatabase.LoadAssetAtPath<TilesSO>(path);
        }

        // 수정된 TilesSO를 저장하는 함수
        private void SaveTilesSO()
        {
            if (selectedTilesSO != null)
            {
                EditorUtility.SetDirty(selectedTilesSO);
                AssetDatabase.SaveAssets();
                Debug.Log($"TilesSO '{selectedTilesSO.name}' saved successfully!");
            }
            else
            {
                Debug.LogWarning("No TilesSO selected to save.");
            }
        }

        // TilesSO 에셋을 삭제하는 함수
        private void DeleteTilesSO()
        {
            if (selectedTilesSO != null)
            {
                string path = AssetDatabase.GetAssetPath(selectedTilesSO);
                if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete this TilesSO?", "Yes",
                        "No"))
                {
                    AssetDatabase.DeleteAsset(path);
                    selectedTilesSO = null; // 선택된 TilesSO 초기화
                    Debug.Log("TilesSO deleted successfully!");
                }
            }
            else
            {
                Debug.LogWarning("No TilesSO selected to delete.");
            }
        }
    }
}