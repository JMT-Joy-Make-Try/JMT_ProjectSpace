using JMT.Agent.NPC;
using JMT.Core.Manager;
using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCDeadState : State<NPCState>
    {
        private NPCAgent agent;
        public override void EnterState()
        {
            base.EnterState();
            agent.ChangeCloth(AgentType.Patient);
            Debug.Log(BuildingManager.Instance.HospitalBuilding);
            agent.SetBuilding(BuildingManager.Instance.HospitalBuilding);
            if (agent.CurrentWorkingBuilding == null)
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
            agent.InitStat();
            _stateMachine.ChangeState(NPCState.Idle);
        }
    }
}