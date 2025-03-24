using JMT.Agent.Alien;
using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class AlienIdleState : State<AlienState>
    {
        public override void EnterState()
        {
            base.EnterState();
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(1f);
            _stateMachine.ChangeState(AlienState.Move);
        }
    }
}