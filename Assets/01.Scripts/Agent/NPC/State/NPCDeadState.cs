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
            
            var hospitals = BuildingManager.Instance.HospitalBuildings;
            var oxygenBuildings = BuildingManager.Instance.OxygenBuildings;
            var lodgingBuildings = BuildingManager.Instance.LodgingBuildings;
            
            var hospitalBuilding = hospitals[Random.Range(0, hospitals.Count)];
            var oxygenBuilding = oxygenBuildings[Random.Range(0, oxygenBuildings.Count)];
            var lodgingBuilding = lodgingBuildings[Random.Range(0, lodgingBuildings.Count)];

            if (TryAssignAndMoveToBuilding(
                condition: agent.HealthCompo.IsDead,
                building: hospitalBuilding,
                onComplete: StartHealingCoroutine))
                return;

            if (TryAssignAndMoveToBuilding(
                condition: agent.OxygenCompo.IsOxygenLow,
                building: oxygenBuilding,
                onComplete: StartOxygenCoroutine))
                return;

            if (TryAssignAndMoveToBuilding(
                condition: true,
                building: lodgingBuilding,
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
            var oxygenBuildings = BuildingManager.Instance.OxygenBuildings;
            var oxygenBuilding = oxygenBuildings[Random.Range(0, oxygenBuildings.Count)];

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
            var hospitals = BuildingManager.Instance.HospitalBuildings;
            var hospital = hospitals[Random.Range(0, hospitals.Count)];
            yield return new WaitForSeconds(hospital.HealingTime);

            agent.Init();
            agent.ClothCompo.ChangeCloth(AgentType.Base);
            _stateMachine.ChangeState(NPCState.Idle);
        }
    }
}
