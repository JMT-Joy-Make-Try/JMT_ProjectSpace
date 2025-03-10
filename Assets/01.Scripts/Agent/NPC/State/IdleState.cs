using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class IdleState : State<NPCState>
    {
        
        public override void EnterState()
        {
            base.EnterState();
            Debug.Log("Enter Idle State");
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            yield return new WaitForSeconds(5f);
            _agent.stateMachine.ChangeState(NPCState.Move);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("Update Idle State");
        }
        
        public override void ExitState()
        {
            base.ExitState();
            Debug.Log("Exit Idle State");
        }
    }
}