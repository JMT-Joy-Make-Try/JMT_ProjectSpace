using JMT.Building.Component;
using JMT.Agent.NPC;
using JMT.Core.Tool;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JMT.Agent.State
{
    public class NPCWorkState : State<NPCState>
    {
        [SerializeField] private NPCWorkAnimationData[] animationData;
        private NPCAgent npcAgent;
        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            npcAgent = agent as NPCAgent;
        }

        public override void EnterState()
        {
            base.EnterState();
            npcAgent.MovementCompo.Stop(true);
            npcAgent.transform.rotation = Quaternion.Euler(0, 0, 0);
            npcAgent.transform.localRotation = Quaternion.Euler(0, 0, 0);
            npcAgent.WorkCompo.CurrentWorkingBuilding.GetBuildingComponent<BuildingWorker>().Work();
            
            int index = Random.Range(0, animationData.Length);
            //ChangeAnimations(animationData[index]);
        }

        private void ChangeAnimations(NPCWorkAnimationData npcWorkAnimationData)
        {
            var animator = npcAgent.AnimatorCompo;
            animator.ChangeAnimation("InWork", npcWorkAnimationData.InClip);
            animator.ChangeAnimation("OutWork", npcWorkAnimationData.OutClip);
            animator.ChangeAnimation("Work", npcWorkAnimationData.WorkClip);
        }

        public override void UpdateState()
        {
            npcAgent.transform.localPosition = npcAgent.WorkCompo.CurrentWorkingBuilding.GetBuildingComponent<BuildingNPC>().WorkPosition.position;
            base.UpdateState();
            npcAgent.transform.rotation = Quaternion.Euler(0, 180, 0);
            npcAgent.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        public override void ExitState()
        {
            npcAgent.MovementCompo.Stop(false);
            base.ExitState();
        }
    }


    [Serializable]
    public struct NPCWorkAnimationData
    {
        public AnimationClip InClip;
        public AnimationClip OutClip;
        public AnimationClip WorkClip;
    }
}