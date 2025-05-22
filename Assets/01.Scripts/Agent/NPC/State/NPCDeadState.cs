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
        private NPCMovement movementCompo;

        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            this.agent = agent as NPCAgent;
            movementCompo = agent.MovementCompo as NPCMovement;
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

        private bool TryAssignAndMoveToBuilding(bool condition, BuildingBase building, System.Action<BuildingBase> onComplete)
        {
            if (!condition || building == null)
                return false;

            if (agent.WorkCompo.CurrentWorkingBuilding != building)
            {
                agent.WorkCompo.SetBuilding(building);
                Debug.Log($"{building.name} Assigned");
            }

            var targetPos = building.GetBuildingComponent<BuildingNPC>().WorkPosition.position;
            movementCompo.SetBuilding(building);
            movementCompo.Move(targetPos, agent.Health.MoveSpeed, onComplete);

            return true;
        }

        private void StartLodgingCoroutine(BuildingBase building)
        {
            StartCoroutine(LodgingRoutine(building as LodgingBuilding));
        }

        private IEnumerator LodgingRoutine(LodgingBuilding building)
        {
            yield return new WaitUntil(() => agent.MovementCompo.IsMoving);
            PoolingManager.Instance.Push(agent);
        }

        private void StartOxygenCoroutine(BuildingBase building)
        {
            StartCoroutine(OxygenRoutine(building as OxygenBuilding));
        }

        private IEnumerator OxygenRoutine(OxygenBuilding building)
        {
            var wait = new WaitForSeconds(1f);

            while (!building.GetOxygen())
                yield return wait;

            agent.OxygenCompo.InitOxygen();
            agent.ClothCompo.ChangeCloth(AgentType.Base);
            _stateMachine.ChangeState(NPCState.Idle);
        }

        private void StartHealingCoroutine(BuildingBase building)
        {
            StartCoroutine(HealingRoutine(building as HospitalBuilding));
        }

        private IEnumerator HealingRoutine(HospitalBuilding building)
        {
            yield return new WaitForSeconds(building.HealingTime);

            agent.Init();
            agent.ClothCompo.ChangeCloth(AgentType.Base);
            _stateMachine.ChangeState(NPCState.Idle);
        }
    }
}
