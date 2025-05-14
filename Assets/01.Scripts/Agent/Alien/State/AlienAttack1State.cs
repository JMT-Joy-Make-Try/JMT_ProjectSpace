using JMT.Agent.Alien;
using UnityEngine;

namespace JMT.Agent.State
{
    public class AlienAttack1State : State<AlienState>
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
            Agent.MovementCompo.Stop(true);
            if (_alien.TargetFinder.Target == null) Debug.LogWarning("No target found");
            Vector3 lookDirection = _alien.TargetFinder.Target.position - _alien.transform.position;
            Agent.transform.rotation = Quaternion.LookRotation(lookDirection);
            
            _alien.Attacker.Attack();
        }

        public override void OnAnimationEnd()
        {
            Agent.StateMachineCompo.ChangeState(AlienState.Follow);
            _alien.TargetFinder.Target = null;
            Debug.Log("AlienAttack1State.OnAnimationEnd");
        }
    }
}