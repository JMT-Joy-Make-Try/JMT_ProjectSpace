using JMT.Building.Component;
using UnityEditor;
using UnityEngine;

namespace JMT.Building.Editor
{
    [CustomEditor(typeof(BuildingBase), true)]
    public class BuildingComponentAdd : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Add Building Component"))
            {
                var building = target as BuildingBase;
                if (building == null)
                {
                    Debug.LogError("Target is not a BuildingBase");
                    return;
                }

                // Undo 등록 및 컴포넌트 추가
                AddComponentWithUndo<BuildingVisual>(building.gameObject);
                AddComponentWithUndo<BuildingAnimator>(building.gameObject);
                AddComponentWithUndo<BuildingNPC>(building.gameObject);
                AddComponentWithUndo<BuildingHealth>(building.gameObject);
                AddComponentWithUndo<BuildingLevel>(building.gameObject);
                AddComponentWithUndo<BuildingData>(building.gameObject);

                // 변경 사항 저장
                EditorUtility.SetDirty(building);
            }
        }

        private void AddComponentWithUndo<T>(GameObject obj) where T : UnityEngine.Component
        {
            if (obj.GetComponent<T>() == null)
            {
                Undo.RecordObject(obj, "Add Component");
                var comp = obj.AddComponent<T>();
                EditorUtility.SetDirty(obj);
                EditorUtility.SetDirty(comp); // 추가한 컴포넌트도 dirty 처리
            }
        }

    }
}