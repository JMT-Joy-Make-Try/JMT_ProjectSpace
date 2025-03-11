using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class IdleState : State<NPCState>
    {
        
        public override void EnterState()
        {
            base.EnterState();
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            yield return new WaitForSeconds(5f);
            _agent.StateMachineCompo.ChangeState(NPCState.Move);
        }
    }
}