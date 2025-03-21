using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace JMT.Agent.Editor
{
    [CustomEditor(typeof(StateMachine<>), true)]
    public class StateMachineCustomEditor : UnityEditor.Editor
    {
        private Transform _parent;
        private bool _isAlien;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            _isAlien = EditorGUILayout.Toggle("Is Alien", _isAlien);

            if (GUILayout.Button("Add State"))
            {
                GenerateState();
            }
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