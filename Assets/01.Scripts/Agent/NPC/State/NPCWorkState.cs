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
}