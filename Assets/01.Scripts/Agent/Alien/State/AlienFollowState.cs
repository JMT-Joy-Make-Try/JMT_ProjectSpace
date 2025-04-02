using JMT.Agent.Alien;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
            Agent.MovementCompo.Stop(false);
            base.EnterState();
        }

        public override void UpdateState()
        {
            if (_alien.TargetFinder.Target == null)
            {
                Agent.MovementCompo.Move(Vector3.zero, _alien.MoveSpeed);
                return;
            }
            Agent.MovementCompo.Move(_alien.TargetFinder.Target.position, _alien.MoveSpeed);
            if (Vector3.Distance(_alien.TargetFinder.Target.position, Agent.transform.position) < _alien.Attacker.AttackRange)
            {
                Agent.MovementCompo.Stop(true);
                int attackState = Random.Range(2, 5);
                Agent.StateMachineCompo.ChangeState((AlienState)attackState);
            }
        }
    }
}