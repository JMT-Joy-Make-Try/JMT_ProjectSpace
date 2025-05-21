using JMT.Agent.NPC;
using JMT.Building;
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

            agent.ClothCompo.ChangeCloth(AgentType.Patient);

            if (TryAssignAndMoveToBuilding(
                condition: agent.HealthCompo.IsDead,
                building: BuildingManager.Instance.HospitalBuilding,
                onComplete: StartHealingCoroutine))
                return;

            if (TryAssignAndMoveToBuilding(
                condition: agent.OxygenCompo.IsOxygenLow,
                building: BuildingManager.Instance.OxygenBuilding,
                onComplete: StartOxygenCoroutine))
                return;

            if (TryAssignAndMoveToBuilding(
                condition: true,
                building: BuildingManager.Instance.LodgingBuilding,
                onComplete: StartLodgingCoroutine))
                return;

            _stateMachine.ChangeState(NPCState.Move);
        }

        private bool TryAssignAndMoveToBuilding(bool condition, BuildingBase building, System.Action onComplete)
        {
            if (!condition || building == null)
                return false;

            if (agent.WorkCompo.CurrentWorkingBuilding != building)
            {
                agent.WorkCompo.SetBuilding(building);
                Debug.Log($"{building.name} Assigned");
            }

            var targetPos = building.GetBuildingComponent<BuildingNPC>().WorkPosition.position;
            agent.MovementCompo.Move(targetPos, agent.Health.MoveSpeed, onComplete);

            return true;
        }

        private void StartLodgingCoroutine()
        {
            StartCoroutine(LodgingRoutine());
        }

        private IEnumerator LodgingRoutine()
        {
            yield return new WaitUntil(() => agent.MovementCompo.IsMoving);
            PoolingManager.Instance.Push(agent);
        }

        private void StartOxygenCoroutine()
        {
            StartCoroutine(OxygenRoutine());
        }

        private IEnumerator OxygenRoutine()
        {
            var wait = new WaitForSeconds(1f);
            var oxygenBuilding = BuildingManager.Instance.OxygenBuilding;

            while (!oxygenBuilding.GetOxygen())
                yield return wait;

            agent.OxygenCompo.InitOxygen();
            agent.ClothCompo.ChangeCloth(AgentType.Base);
            _stateMachine.ChangeState(NPCState.Idle);
        }

        private void StartHealingCoroutine()
        {
            StartCoroutine(HealingRoutine());
        }

        private IEnumerator HealingRoutine()
        {
            var hospital = BuildingManager.Instance.HospitalBuilding;
            yield return new WaitForSeconds(hospital.HealingTime);

            agent.Init();
            agent.ClothCompo.ChangeCloth(AgentType.Base);
            _stateMachine.ChangeState(NPCState.Idle);
        }
    }
}
