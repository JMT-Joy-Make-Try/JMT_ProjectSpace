using JMT.Core.Tool.PoolManager.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace JMT.Core.Tool.PoolingSystem.Editors
{
    public partial class UtilityWindow
    {
        private PoolingTableSO _effectTable;


        /**
         * PoolManager의 PoolTable 설정
         */
        private void EffectSetting()
        {
            #region Effect Setting

            if (_effectTable == null)
            {
                _effectTable = AssetDatabase.LoadAssetAtPath<PoolingTableSO>
                    ($"{_poolDirectory}/table_effect.asset");
                if (_effectTable == null)
                {
                    _effectTable = CreateInstance<PoolingTableSO>();
                    string fileName = AssetDatabase.GenerateUniqueAssetPath
                        ($"{_poolDirectory}/table_effect.asset");

                    AssetDatabase.CreateAsset(_effectTable, fileName);
                    Debug.Log($"Create Pooling Table at {fileName}(Effect)");
                }
            }

            #endregion
        }


        /**
         * 사각형 정보 알아오기
         */

        /**
         * Pool Table 그리기
         */
        private void DrawEffectTable()
        {
            foreach (PoolingItemSO item in _effectTable.datas)
            {
                // 현재 그릴 item이 선택아이템과 동일하면 스타일 지정
                GUIStyle style = selectedItem[UtilType.Agent] == item
                    ? _selectStyle
                    : GUIStyle.none;
                EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                {
                    EditorGUILayout.LabelField(item.enumName,
                        GUILayout.Height(40f), GUILayout.Width(240f));

                    PoolItemDeleteButton(item, _effectTable);

                }
                EditorGUILayout.EndHorizontal();

                GetRect(item, UtilType.Effect);

                // 삭제 확인 break;
                if (item == null)
                    break;
            }
        }


        private void GenerateEnumFile()
        {
            string path = $"{Application.dataPath}/01.Scripts/Core/Tool/PoolManager/Core/ObjectPool/PoolingType.cs";
            Debug.Log(path);
            HashSet<string> enumEntries = new HashSet<string>();

            // 기존 파일이 존재하면 기존 Enum 항목을 읽어옴
            if (File.Exists(path))
            {
                Debug.Log($"File exists at {path}");
                string existingCode = File.ReadAllText(path);
                Match match = Regex.Match(existingCode, @"enum PoolingType\s*{([^}]*)}");
                if (match.Success)
                {
                    Debug.Log($"Enum file exists at {path}");
                    string[] existingEntries = match.Groups[1].Value.Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string entry in existingEntries)
                    {
                        string trimmedEntry = entry.Trim();
                        if (!string.IsNullOrEmpty(trimmedEntry))
                        {
                            enumEntries.Add(trimmedEntry);
                        }
                    }
                }
            }

            // 새로운 항목 추가
            foreach (PoolingItemSO item in _poolTable.datas)
            {
                enumEntries.Add(item.enumName);
            }
            
            foreach (PoolingItemSO item in _effectTable.datas)
            {
                enumEntries.Add(item.enumName);
            }

            // 코드 생성
            StringBuilder codeBuilder = new StringBuilder();
            foreach (string entry in enumEntries)
            {
                codeBuilder.AppendLine($"        {entry},");
            }

            string code = string.Format(CodeFormat.PoolingTypeFormat, codeBuilder.ToString().TrimEnd(','));
            Debug.Log(code);
    
            File.WriteAllText(path, code);
            AssetDatabase.Refresh();
        }


        /**
         * 풀 그리기
         */
        private void DrawEffectItems()
        {
            MenuSetting(UtilType.Effect);

            GUI.color = Color.white; //원래 색상으로 복귀.

            EditorGUILayout.BeginHorizontal();
            {
                #region Pooling List

                EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
                {
                    EditorGUILayout.LabelField("Pooling list");
                    EditorGUILayout.Space(3f);

                    scrollPositions[UtilType.Effect] = EditorGUILayout.BeginScrollView
                    (scrollPositions[UtilType.Effect], false, true,
                        GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                    {
                        DrawEffectTable();
                    }
                    EditorGUILayout.EndScrollView();
                }
                EditorGUILayout.EndVertical();

                #endregion

                // 인스펙터 그리기
                DrawInspector(UtilType.Effect);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}