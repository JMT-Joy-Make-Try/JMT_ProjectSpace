using JMT.Agent.NPC;
using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.Core.Tool.PoolManager.Core;
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
                Debug.Log("Hospital Building Added!");
                if (agent.CurrentWorkingBuilding == null ||
                    agent.CurrentWorkingBuilding != BuildingManager.Instance.HospitalBuilding || BuildingManager.Instance.HospitalBuilding == null)
                {
                    agent.OxygenCompo.AddOxygen(2);
                    _stateMachine.ChangeState(NPCState.Move);
                    return;
                }
            }
            if (agent.OxygenCompo.IsOxygenLow)
            {
                agent.SetBuilding(BuildingManager.Instance.OxygenBuilding);
                Debug.Log("Oxygen Building Added!");
                if (agent.CurrentWorkingBuilding == null || agent.CurrentWorkingBuilding != BuildingManager.Instance.OxygenBuilding || BuildingManager.Instance.OxygenBuilding == null)
                {
                    agent.OxygenCompo.AddOxygen(2);
                    _stateMachine.ChangeState(NPCState.Move);
                    return;
                }
            }

            if (agent.CurrentWorkingBuilding == null && BuildingManager.Instance.LodgingBuilding != null)
            {
                agent.SetBuilding(BuildingManager.Instance.LodgingBuilding);
                Debug.Log("Lodging Building Added!");
                StartCoroutine(InLodgingBuilding());
            }
            

            agent.MovementCompo.Move(agent.CurrentWorkingBuilding.GetBuildingComponent<BuildingNPC>().WorkPosition.position, agent.MoveSpeed, Heal);
        }

        private IEnumerator InLodgingBuilding()
        {
            yield return new WaitUntil(() => agent.MovementCompo.IsMoving);
            PoolingManager.Instance.Push(agent);
        }

        private void Heal()
        {
            if (agent.IsDead)
            {
                if (BuildingManager.Instance.HospitalBuilding != null)
                {
                    StartCoroutine(HealCoroutine());
                }
                else
                {
                    _stateMachine.ChangeState(NPCState.Move, true);
                }
            }
            else if (agent.OxygenCompo.IsOxygenLow)
            {
                if (BuildingManager.Instance.OxygenBuilding != null)
                {
                    StartCoroutine(OxygenRestore());
                }
                else
                {
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