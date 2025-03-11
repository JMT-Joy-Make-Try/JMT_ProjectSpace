using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.State
{
    public class MoveState : State<NPCState>
    {
        public override void EnterState()
        {
            base.EnterState();
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            _agent.MovementCompo.Move(new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f)), 10);
            yield return new WaitForSeconds(5f);
            _agent.StateMachineCompo.ChangeState(NPCState.Idle);
        }
    }
}