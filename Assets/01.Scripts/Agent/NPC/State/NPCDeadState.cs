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
            if (agent.IsDead)
            {
                agent.SetBuilding(BuildingManager.Instance.HospitalBuilding);
                if (agent.CurrentWorkingBuilding == null ||
                    agent.CurrentWorkingBuilding != BuildingManager.Instance.HospitalBuilding || BuildingManager.Instance.HospitalBuilding == null)
                {
                    _stateMachine.ChangeState(NPCState.Move);
                    return;
                }
            }
            if (agent.OxygenCompo.IsOxygenLow)
            {
                agent.SetBuilding(BuildingManager.Instance.OxygenBuilding);
                if (agent.CurrentWorkingBuilding == null || agent.CurrentWorkingBuilding != BuildingManager.Instance.OxygenBuilding || BuildingManager.Instance.OxygenBuilding == null)
                {
                    _stateMachine.ChangeState(NPCState.Move);
                    return;
                }
            }
            

            agent.MovementCompo.Move(agent.CurrentWorkingBuilding.WorkPosition.position, agent.MoveSpeed, Heal);
        }

        private void Heal()
        {
            if (agent.IsDead)
            {
                if (BuildingManager.Instance.HospitalBuilding != null)
                {
                    Debug.LogError("Asdfafasfa");
                    StartCoroutine(HealCoroutine());
                }
                else
                {
                    Debug.LogError("AsdfafasfaaDS");
                    _stateMachine.ChangeState(NPCState.Move, true);
                }
            }
            else if (agent.OxygenCompo.IsOxygenLow)
            {
                if (BuildingManager.Instance.OxygenBuilding != null)
                {
                    Debug.LogError("AsdfafasfaFASFDF");
                    StartCoroutine(OxygenRestore());
                }
                else
                {
                    Debug.LogError("AsdfafasfaFASFASD");
                    _stateMachine.ChangeState(NPCState.Move);
                }
            }
            
        }

        private IEnumerator OxygenRestore()
        {
            var ws = new WaitForSeconds(1f);
            while (!BuildingManager.Instance.OxygenBuilding.GetOxygen())
            {
                yield return ws;
            }
            agent.OxygenCompo.InitOxygen();
            agent.ChangeCloth(AgentType.Base);
            _stateMachine.ChangeState(NPCState.Idle);
        }

        private IEnumerator HealCoroutine()
        {
            yield return new WaitForSeconds(BuildingManager.Instance.HospitalBuilding.HealingTime);
            agent.Init();
            agent.ChangeCloth(AgentType.Base);
        }
    }
}