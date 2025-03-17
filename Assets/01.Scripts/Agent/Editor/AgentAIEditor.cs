using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace JMT.Agent.Editor
{
    [CustomEditor(typeof(AgentAI<>), true)]
    public class AgentAIEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Generate Animator Controller"))
            {
                GenerateAnimator();
            }
        }

        private void GenerateAnimator()
        {
            var agentAI = (target as MonoBehaviour);
            if (agentAI == null) return;

            Type agentType = agentAI.GetType();
            Type enumType = agentType.BaseType?.GetGenericArguments().FirstOrDefault();

            if (enumType == null || !enumType.IsEnum)
            {
                Debug.LogError("T 타입을 찾을 수 없거나 Enum이 아닙니다.");
                return;
            }

            Animator animator = agentAI.GetComponent<Animator>();
            if (animator == null)
            {
                animator = agentAI.GetComponentInChildren<Animator>();
            }

            string directoryPath = "Assets/04.Animations/";
            string controllerPath = $"{directoryPath}{agentAI.name}_Animator.controller";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                AssetDatabase.Refresh();
            }

            AnimatorController controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
            if (controller == null)
            {
                controller = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
                AssetDatabase.Refresh();
            }

            animator.runtimeAnimatorController = controller;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var rootStateMachine = controller.layers[0].stateMachine;
            var existingStates = rootStateMachine.states.Select(s => s.state.name).ToHashSet();
            var existingParams = controller.parameters.Select(p => p.name).ToHashSet();

            string defaultStateName = Enum.GetNames(enumType).First();

            if (!existingParams.Contains(defaultStateName))
            {
                controller.AddParameter(defaultStateName, AnimatorControllerParameterType.Bool);
            }

            if (!existingStates.Contains(defaultStateName))
            {
                var defaultState = rootStateMachine.AddState(defaultStateName);
                defaultState.motion = GetOrCreateAnimationClip(directoryPath, agentAI.name, defaultStateName);
                var transition = rootStateMachine.AddAnyStateTransition(defaultState);
                transition.AddCondition(AnimatorConditionMode.If, 0, defaultStateName);
                transition.duration = 0f;
                transition.hasExitTime = true;
                transition.exitTime = 0f;
                transition.hasExitTime = false;
                transition.canTransitionToSelf = false;
                
                // var exitTransition = defaultState.AddTransition(rootStateMachine.states.FirstOrDefault(s => s.state.name == "Any State").state);
                // exitTransition.AddCondition(AnimatorConditionMode.IfNot, 0, defaultStateName);
            }

            foreach (var value in Enum.GetValues(enumType))
            {
                string stateName = value.ToString();
                if (stateName == defaultStateName) continue;

                if (!existingParams.Contains(stateName))
                {
                    controller.AddParameter(stateName, AnimatorControllerParameterType.Bool);
                }

                if (!existingStates.Contains(stateName))
                {
                    var state = rootStateMachine.AddState(stateName);
                    state.motion = GetOrCreateAnimationClip(directoryPath,agentAI.name, stateName);

                    // AnyState → 해당 상태 전환
                    var transition = rootStateMachine.AddAnyStateTransition(state);
                    transition.AddCondition(AnimatorConditionMode.If, 0, stateName);
                    transition.duration = 0f;
                    transition.hasExitTime = true;
                    transition.exitTime = 0f;
                    transition.hasExitTime = false;
                    transition.canTransitionToSelf = false;

                    // // Exit 노드로의 연결 설정 (Exit 상태는 AnyState의 기본 Exit에 연결되어 있음)
                    // var exitTransition = state.AddTransition(rootStateMachine.states.FirstOrDefault(s => s.state.name == "Any State").state);
                    // exitTransition.AddCondition(AnimatorConditionMode.IfNot, 0, stateName);
                    // exitTransition.duration = 0.1f;
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"Animator Controller 생성 완료: {controllerPath}");
        }

        private AnimationClip GetOrCreateAnimationClip(string directoryPath, string namePath, string stateName)
        {
            string clipName = $"{stateName}.anim";
            string clipPath = $"{directoryPath}/{namePath}/";
            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(clipPath + clipName);
            
            if (!Directory.Exists(clipPath))
            {
                Directory.CreateDirectory(clipPath);
                AssetDatabase.Refresh();
            }

            if (clip == null)
            {
                clip = new AnimationClip();
                // Clip Loop Time on
                AssetDatabase.CreateAsset(clip, clipPath + clipName);
                AssetDatabase.Refresh();
            }
            SetClipSettings(clip);
            return clip;
        }

        private void SetClipSettings(AnimationClip clip)
        {
            AnimationClipSettings clipSettings = AnimationUtility.GetAnimationClipSettings(clip);
            clipSettings.loopTime = true;
            AnimationUtility.SetAnimationClipSettings(clip, clipSettings);
        }
    }
}
