﻿using JMT.Agent.NPC;
using JMT.Building;
using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.Core.Tool.PoolManager.Core;
using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCMoveState : State<NPCState>
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float _distance = 10f;
        private NPCAgent _agent;
        private Vector3 _targetPosition;

        private Coroutine _moveCoroutine;

        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            this._agent = (NPCAgent)agent;
            this._agent.OnTypeChanged += HandleTypeChanged;
            _agent.OxygenCompo.OnOxygenLowEvent += HandleOxygenLow;
        }

        private void OnDestroy()
        {
            _agent.OnTypeChanged -= HandleTypeChanged;
            _agent.OxygenCompo.OnOxygenLowEvent -= HandleOxygenLow;
        }

        private void HandleOxygenLow()
        {
            _stateMachine.ChangeState(NPCState.Dead);
        }

        private void HandleTypeChanged(AgentType obj)
        {
            int multiplier = 1;

            if (obj == AgentType.Patient || obj == AgentType.Base)
            {
                multiplier = 1;
                BuildingManager buildingManager = BuildingManager.Instance;
                if (buildingManager.LodgingBuildings.Count > 0)
                {
                    var lodgingBuilding = buildingManager.LodgingBuildings[Random.Range(0, buildingManager.LodgingBuildings.Count)];
                    if (lodgingBuilding != null)
                    {
                        _targetPosition = lodgingBuilding.transform.position;
                    }
                }
                else if (buildingManager.HospitalBuildings.Count > 0)
                {
                    var hospitalBuilding = buildingManager.HospitalBuildings[Random.Range(0, buildingManager.HospitalBuildings.Count)];
                    if (hospitalBuilding != null)
                    {
                        _targetPosition = hospitalBuilding.transform.position;
                    }
                }
            }
            else
            {
                multiplier = 2;
                var curWorkingBuilding = _agent.WorkCompo.CurrentWorkingBuilding;
                if (curWorkingBuilding == null)
                {
                    Debug.LogError("현재 작업중인 건물이 없습니다.");
                    return;
                }

                Vector3 pos = curWorkingBuilding.GetBuildingComponent<BuildingNPC>().WorkPosition.position;
                _targetPosition = pos;
            }

            _agent.MovementCompo.Move(_targetPosition, _agent.Health.MoveSpeed * multiplier, () => EndMove(obj));
        }

        private IEnumerator LodgingBuildingRoutine()
        {
            yield return new WaitUntil(() => !_agent.MovementCompo.IsMoving);
            PoolingManager.Instance.Push(_agent);
        }

        private void EndMove(AgentType type)
        {
            // todo : fix this
            if (type is AgentType.Base or AgentType.Patient)
            {
                if (_targetPosition == ((NPCMovement)_agent.MovementCompo).Building.GetComponent<BuildingNPC>().WorkPosition.position)
                {
                    StartCoroutine(LodgingBuildingRoutine());
                }
            }
            _agent.transform.rotation = Quaternion.identity;
            _agent.transform.localRotation = Quaternion.identity;
            if (type != AgentType.Base)
            {
                _agent.StateMachineCompo.ChangeState(NPCState.Work);
                Debug.Log("일해라 인간아");
                _agent.ClothCompo.ChangeCloth(type);
            }
        }
    }
}