using JMT.Agent.Alien;
using UnityEngine;

namespace JMT.Agent.State
{
    public class AlienMoveState : State<AlienState>
    {
        public override void EnterState()
        {
            base.EnterState();
            _agent.MovementCompo.Move(new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f)), 100);
        }
    }
}