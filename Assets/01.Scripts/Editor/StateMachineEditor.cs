using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace JMT.Agent.Editor
{
    [CustomEditor(typeof(StateMachine<>), true)]
    public class StateMachineEditor : UnityEditor.Editor
    {
        private SerializedProperty statesProp;
        private object stateMachineInstance;
        private Type enumType;
        
        private Transform _parent;
        private bool _isAlien;

        private void OnEnable()
        {
            stateMachineInstance = target;
            var type = target.GetType();
            
            // states 필드 찾기
            statesProp = serializedObject.FindProperty("states");

            // T Enum 타입 추출
            enumType = type.BaseType.GetGenericArguments()[0];
        }

        public override void OnInspectorGUI()
        {
            _isAlien = EditorGUILayout.Toggle("Is Alien", _isAlien);

            if (GUILayout.Button("Add State"))
            {
                GenerateState();
            }
            serializedObject.Update();

            EditorGUILayout.PropertyField(statesProp, true);
            EditorGUILayout.Space();

            if (Application.isPlaying)
            {
                DrawRuntimeControls();
            }
            else
            {
                EditorGUILayout.HelpBox("런타임 중에만 FSM 시각화가 활성화됩니다.", MessageType.Info);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawRuntimeControls()
        {
            EditorGUILayout.LabelField("=== FSM 시각화 ===", EditorStyles.boldLabel);

            var method = stateMachineInstance.GetType().GetProperty("CurrentState")?.GetGetMethod();
            var currentState = method?.Invoke(stateMachineInstance, null) as MonoBehaviour;
            EditorGUILayout.LabelField("현재 상태:", currentState ? currentState.GetType().Name : "None");

        }
        
        private void GenerateState()
        {
            if (!(target is MonoBehaviour stateMachine)) return;

            Type enumType = stateMachine.GetType().BaseType?.GetGenericArguments().FirstOrDefault();
            if (enumType == null || !enumType.IsEnum)
            {
                Debug.LogError("[StateMachineEditor] T 타입을 찾을 수 없거나 Enum이 아닙니다.");
                return;
            }

            _parent = stateMachine.transform.Find("State") ?? new GameObject("State").transform;
            _parent.SetParent(stateMachine.transform, false);

            var existingStates = _parent.Cast<Transform>().Select(t => t.name).ToHashSet();
            foreach (string stateName in Enum.GetNames(enumType))
            {
                if (existingStates.Contains(stateName)) continue;

                var stateObject = new GameObject(stateName) { transform = { parent = _parent, localPosition = Vector3.zero} };
                string agentName = _isAlien ? "Alien" : "NPC";
                Type stateClass = GetStateType($"JMT.Agent.State.{agentName}{stateName}State");
                if (stateClass != null) stateObject.AddComponent(stateClass);

                var stateMachineField = stateMachine.GetType();
                Debug.Log(stateMachineField);
                var states = stateMachineField.GetProperty("States", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                Debug.Log(states);
                Debug.Log(enumType);
                
                var dict = (IDictionary)states.GetValue(stateMachine);
                dict?.Add(Enum.Parse(enumType, stateName), stateObject.GetComponent(stateClass));
            }
        }

        private static Type GetStateType(string typeName)
        {
            return Type.GetType(typeName) ?? AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName == typeName);
        }
    }
}
