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

        private void Update()
        {
            _agent.MovementCompo.Move(_alien.Target.position, _alien.MoveSpeed);
        }
    }
}