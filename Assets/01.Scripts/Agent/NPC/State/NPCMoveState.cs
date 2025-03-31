using JMT.Agent.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCMoveState : State<NPCState>
    {
        private NPCAgent agent;
        private Vector3 targetPosition;
        
        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            this.agent = (NPCAgent)agent;
            this.agent.OnTypeChanged += HandleTypeChanged;
        }

        private void HandleTypeChanged(AgentType obj)
        {
            if (obj == AgentType.Base)
            {
                targetPosition = new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f));
            }
            else
            {
                targetPosition = agent.CurrentWorkingBuilding.WorkPosition.position;
            }
            agent.MovementCompo.Move(targetPosition, agent.MoveSpeed, () => EndMove(obj));
        }

        private void EndMove(AgentType type)
        {
            agent.transform.rotation = agent.CurrentWorkingBuilding.WorkPosition.rotation;
            if (type != AgentType.Base)
            {
                agent.StateMachineCompo.ChangeState(NPCState.Work);
                Debug.Log("일해라 인간아");
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
                agent.MovementCompo.Move(targetPosition, agent.MoveSpeed);
                yield return new WaitUntil(() => !Agent.MovementCompo.IsMoving);
            }
        }
    }
}