using JMT.Agent.Alien;
using UnityEngine;

namespace JMT.Agent.State
{
    public class AlienAttack2State : State<AlienState>
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
            Agent.MovementCompo.Stop(false);
            _alien.transform.LookAt(_alien.TargetFinder.Target);
            
            _alien.Attacker.Attack();
        }

        public override void OnAnimationEnd()
        {
            Agent.StateMachineCompo.ChangeState(AlienState.Follow);
            _alien.TargetFinder.Target = null;
            Debug.Log("AlienAttack2State.OnAnimationEnd");
        }
    }
}