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
            StartCoroutine(MoveBuilding());
        }

        private IEnumerator MoveBuilding()
        {
            yield return new WaitForSeconds(npcAgent.WorkSpeed * 0.2f);
            Debug.Log("Work");
            Agent.MovementCompo.Move(npcAgent.CurrentWorkingBuilding.transform.position, npcAgent.WorkSpeed);
            Agent.StateMachineCompo.ChangeStateDelay(NPCState.Move, npcAgent.WorkSpeed);
        }
    }
}