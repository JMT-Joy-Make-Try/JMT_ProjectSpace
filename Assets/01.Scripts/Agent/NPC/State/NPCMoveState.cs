using JMT.Agent.NPC;
using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCMoveState : State<NPCState>
    {
        private NPCAgent _agent;
        private Vector3 _targetPosition;
        
        private Coroutine _moveCoroutine;
        
        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            this._agent = (NPCAgent)agent;
            this._agent.OnTypeChanged += HandleTypeChanged;
        }

        private void HandleTypeChanged(AgentType obj)
        {
            if (obj == AgentType.Base || obj == AgentType.Patient)
            {
                _targetPosition = new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f));
            }
            else
            {
                _targetPosition = _agent.CurrentWorkingBuilding.WorkPosition.position;
            }
            _agent.MovementCompo.Move(_targetPosition, _agent.MoveSpeed, () => EndMove(obj));
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
                _agent.MovementCompo.Move(_targetPosition, _agent.MoveSpeed);
                yield return new WaitUntil(() => !Agent.MovementCompo.IsMoving);
            }
        }
    }
}