using JMT.Agent.Alien;

namespace JMT.Agent.State
{
    public class AlienAttackState : State<AlienState>
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
            _agent.MovementCompo.Move(_alien.Target.position, _alien.MoveSpeed);
        }
    }
}