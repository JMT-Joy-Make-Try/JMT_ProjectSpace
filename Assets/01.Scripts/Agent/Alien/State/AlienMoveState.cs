using JMT.Agent.Alien;
using UnityEngine;

namespace JMT.Agent.State
{
    public class AlienMoveState : State<AlienState>
    {
        public override void EnterState()
        {
            base.EnterState();
            _agent.MovementCompo.Move(Vector3.zero, ((Alien.Alien)_agent).MoveSpeed);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            DetectObject();
        }

        private void DetectObject()
        {
        }
    }
}