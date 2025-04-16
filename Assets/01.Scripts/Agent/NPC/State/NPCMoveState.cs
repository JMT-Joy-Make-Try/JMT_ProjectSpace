using JMT.Agent.NPC;
using JMT.Planets.Tile;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
            if (obj == AgentType.Base || obj == AgentType.Patient)
            {
                multiplier = 1;
                _targetPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            }
            else
            {
                multiplier = 2;
                _targetPosition = _agent.CurrentWorkingBuilding.WorkPosition.position;
            }
            _agent.MovementCompo.Move(_targetPosition, _agent.MoveSpeed * multiplier, () => EndMove(obj));
        }

        private void EndMove(AgentType type)
        {
            if (type == AgentType.Patient) return;
            _agent.transform.rotation = Quaternion.identity;
            _agent.transform.localRotation = Quaternion.identity;
            Debug.Log(_agent.transform.rotation);
            Debug.Log(_agent.transform.localRotation);
            Debug.Log(_agent.name);
            if (type != AgentType.Base)
            {
                _agent.StateMachineCompo.ChangeState(NPCState.Work);
                Debug.Log("일해라 인간아");
                _agent.ChangeCloth(type);
            }
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
                _targetPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                _agent.MovementCompo.Move(_targetPosition, _agent.MoveSpeed);
                yield return new WaitUntil(() => !_agent.MovementCompo.IsMoving);
            }
        }

    }
}