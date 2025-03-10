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
            Debug.Log("Enter Move State");
            
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            yield return new WaitForSeconds(5f);
            _agent.stateMachine.ChangeState(NPCState.Idle);
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("Update Move State");
            _agent.transform.position += _agent.transform.forward * Time.deltaTime;
        }
        
        public override void ExitState()
        {
            base.ExitState();
            Debug.Log("Exit Move State");
        }
    }
}