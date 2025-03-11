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
            _agent.MovementCompo.Move(new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), 5);
            yield return new WaitForSeconds(5f);
            _agent.StateMachineCompo.ChangeState(NPCState.Idle);
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("Update Move State");
            
        }
        
        public override void ExitState()
        {
            base.ExitState();
            Debug.Log("Exit Move State");
        }
    }
}