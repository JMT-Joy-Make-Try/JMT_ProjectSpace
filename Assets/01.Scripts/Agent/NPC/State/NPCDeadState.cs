using JMT.Core.Tool.PoolManager.Core;
using JMT.Building.Component;
using System.Collections;
using JMT.Core.Manager;
using JMT.Agent.NPC;
using JMT.Building;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCDeadState : State<NPCState>
    {
        private NPCAgent agent;
        private NPCMovement movementCompo;
        
        private HospitalBuilding hospital;
        private OxygenBuilding oxygen;
        private LodgingBuilding lodging;

        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            this.agent = agent as NPCAgent;
            movementCompo = agent.MovementCompo as NPCMovement;
        }

        public override async void EnterState()
        {
            base.EnterState();
            
            agent.ClothCompo.ChangeCloth(AgentType.Patient);

            hospital = await FindNearbyBuilding<HospitalBuilding>();
            oxygen = await FindNearbyBuilding<OxygenBuilding>();
            lodging = await FindNearbyBuilding<LodgingBuilding>();
            
            if (TryAssignAndMoveToBuilding(
                    condition: agent.HealthCompo.IsDead,
                    building: hospital,
                    onComplete: StartHealingCoroutine))
                return;
            
            if (TryAssignAndMoveToBuilding(
                    condition: agent.OxygenCompo.IsOxygenLow,
                    building: oxygen,
                    onComplete: StartOxygenCoroutine))
                return;
            
            if (TryAssignAndMoveToBuilding(
                    condition: true,
                    building: lodging,
                    onComplete: StartLodgingCoroutine))
                return;
            _stateMachine.ChangeState(NPCState.Move);
        }

        private bool TryAssignAndMoveToBuilding(bool condition, BuildingBase building, System.Action<BuildingBase> onComplete)
        {
            if (!condition || building == null)
            {
                Debug.Log(condition);
                Debug.Log(building);
                return false;
            }

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
            StartCoroutine(LodgingRoutine());
        }

        private IEnumerator LodgingRoutine()
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
            var lodgingBuildings = BuildingManager.Instance.LodgingBuildings;
            var lodgingBuilding = lodgingBuildings[Random.Range(0, lodgingBuildings.Count)];
            agent.MovementCompo.Move(lodgingBuilding.GetBuildingComponent<BuildingNPC>().WorkPosition.position, agent.Health.MoveSpeed);
            agent.ClothCompo.ChangeCloth(AgentType.Base);
            _stateMachine.ChangeState(NPCState.Move);
        }

        private async Awaitable<T> FindNearbyBuilding<T>() where T : BuildingBase
        {
            List<T> buildings = null;
            if (typeof(T) == typeof(HospitalBuilding))
                buildings = BuildingManager.Instance.HospitalBuildings as List<T>;
            else if (typeof(T) == typeof(OxygenBuilding))
                buildings = BuildingManager.Instance.OxygenBuildings as List<T>;
            else if (typeof(T) == typeof(LodgingBuilding))
                buildings = BuildingManager.Instance.LodgingBuildings as List<T>;
        
            if (buildings == null || buildings.Count == 0)
                return null;
        
            Vector3 agentPos = agent.transform.position;
            T nearest = null;
            float minDist = float.MaxValue;
        
            int count = buildings.Count;
            if (count < 10)
            {
                for (int i = 0; i < count; i++)
                {
                    float dist = Vector3.Distance(agentPos, buildings[i].transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearest = buildings[i];
                    }
                }
                return nearest;
            }
        
            await Awaitable.BackgroundThreadAsync();
            for (int i = 0; i < count; i++)
            {
                float dist = Vector3.Distance(agentPos, buildings[i].transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = buildings[i];
                }
            }
            await Awaitable.MainThreadAsync();
            return nearest;
        }

    }
}
