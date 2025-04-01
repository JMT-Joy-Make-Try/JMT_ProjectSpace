using JMT.Agent.NPC;
using System.Collections;
using UnityEngine;

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
            npcAgent.CurrentWorkingBuilding.Work();
        }

        public override void UpdateState()
        {
            npcAgent.transform.position = npcAgent.CurrentWorkingBuilding.WorkPosition.position;
            base.UpdateState();
            npcAgent.transform.rotation = Quaternion.Euler(0, 0, 0);
            npcAgent.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public override void ExitState()
        {
            base.ExitState();
            npcAgent.MovementCompo.Stop(false);
        }
    }
}