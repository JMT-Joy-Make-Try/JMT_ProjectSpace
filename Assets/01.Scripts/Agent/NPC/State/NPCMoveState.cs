using JMT.Agent.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCMoveState : State<NPCState>
    {
        private NPCAgent agent;

        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            this.agent = (NPCAgent)agent;
        }

        public override void EnterState()
        {
            base.EnterState();
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            
            while (true)
            {
                if (agent.AgentType == AgentType.Base)
                {
                    Agent.MovementCompo.Move(new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f)), agent.MoveSpeed);
                }
                else
                {
                    agent.MovementCompo.Move(agent.CurrentWorkingBuilding.transform.position, agent.MoveSpeed);
                    Agent.StateMachineCompo.ChangeStateWait(NPCState.Work, !agent.MovementCompo.IsMoving);
                }

                yield return new WaitUntil(() => !Agent.MovementCompo.IsMoving);
            }
        }
    }
}