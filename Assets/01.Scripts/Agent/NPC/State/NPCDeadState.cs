using JMT.Agent.NPC;
using JMT.Core.Manager;
using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCDeadState : State<NPCState>
    {
        private NPCAgent agent;

        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            this.agent = agent as NPCAgent;
        }

        public override void EnterState()
        {
            base.EnterState();
            agent.ChangeCloth(AgentType.Patient);
            Debug.Log(BuildingManager.Instance.HospitalBuilding);
            agent.SetBuilding(BuildingManager.Instance.HospitalBuilding);
            if (agent.CurrentWorkingBuilding == null || agent.CurrentWorkingBuilding != BuildingManager.Instance.HospitalBuilding)
            {
                _stateMachine.ChangeState(NPCState.Move);
                return;
            }
            agent.MovementCompo.Move(agent.CurrentWorkingBuilding.WorkPosition.position, agent.MoveSpeed, Heal);
        }

        private void Heal()
        {
            StartCoroutine(HealCoroutine());
        }

        private IEnumerator HealCoroutine()
        {
            yield return new WaitForSeconds(BuildingManager.Instance.HospitalBuilding.HealingTime);
            agent.Init();
            agent.ChangeCloth(AgentType.Base);
        }
    }
}