using JMT.Agent.Alien;

namespace JMT.Agent.State
{
    public class AlienAttack3State : State<AlienState>
    {
        private Alien.Alien _alien;

        public override void Initialize(AgentAI<AlienState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _alien = (Alien.Alien)agent;
        }

        public override void EnterState()
        {
            base.EnterState();
            _alien.transform.LookAt(_alien.TargetFinder.Target);
            
            _alien.Attacker.Attack(AttackType.Continuous);
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            _agent.StateMachineCompo.ChangeState(AlienState.Follow);
        }
    }
}