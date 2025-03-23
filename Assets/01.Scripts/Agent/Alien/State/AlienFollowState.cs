using JMT.Agent.Alien;
using System;

namespace JMT.Agent.State
{
    public class AlienFollowState : State<AlienState>
    {
        private Alien.Alien _alien;

        public override void Initialize(AgentAI<AlienState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _alien = (Alien.Alien)agent;
        }

        public override void EnterState()
        {
            _agent.MovementCompo.Stop(false);
            base.EnterState();
            
        }

        public override void UpdateState()
        {
            if (_alien.TargetFinder.Target == null)
            {
                _agent.StateMachineCompo.ChangeState(AlienState.Move);
                return;
            }
            _agent.MovementCompo.Move(_alien.TargetFinder.Target.position, _alien.MoveSpeed);
        }
    }
}