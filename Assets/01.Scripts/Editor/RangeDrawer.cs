using JMT.Core.Tool;
using UnityEditor;
using UnityEngine;

namespace JMT.Editor
{
    [CustomPropertyDrawer(typeof(Range))]
    public class RangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // List에서도 사용 가능하도록 Wrapper 추가
            EditorGUI.BeginProperty(position, label, property);

            // 레이블 표시
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // 각 필드의 너비 계산
            float width = position.width / 3f;
            Rect minRect = new Rect(position.x, position.y + 18, width - 5, position.height - 20);
            Rect maxRect = new Rect(position.x + width, position.y + 18, width - 5, position.height - 20);
            Rect returnRect = new Rect(position.x + width * 2, position.y + 18, width - 5, position.height - 20);

            // 라벨 위치
            Rect minLabelRect = new Rect(position.x, position.y, width - 5, 18);
            Rect maxLabelRect = new Rect(position.x + width, position.y, width - 5, 18);
            Rect returnLabelRect = new Rect(position.x + width * 2, position.y, width - 5, 18);

            // SerializedProperty 가져오기
            SerializedProperty minProp = property.FindPropertyRelative("Min");
            SerializedProperty maxProp = property.FindPropertyRelative("Max");
            SerializedProperty returnProp = property.FindPropertyRelative("ReturnValue");

            // 라벨 그리기
            EditorGUI.LabelField(minLabelRect, "Min", EditorStyles.boldLabel);
            EditorGUI.LabelField(maxLabelRect, "Max", EditorStyles.boldLabel);
            EditorGUI.LabelField(returnLabelRect, "Return", EditorStyles.boldLabel);

            // 입력 필드 그리기
            EditorGUI.PropertyField(minRect, minProp, GUIContent.none);
            EditorGUI.PropertyField(maxRect, maxProp, GUIContent.none);
            EditorGUI.PropertyField(returnRect, returnProp, GUIContent.none);

            EditorGUI.EndProperty();
        }

        // 리스트에서 각 Range의 높이 설정 (기본 높이보다 크게)
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 20;
        }
    }
}
