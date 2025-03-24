using JMT.Agent.Alien;
using JMT.Core;
using JMT.Core.Tool;
using System;
using UnityEngine;

namespace JMT.Agent.State
{
    public class AlienMoveState : State<AlienState>
    {
        private Alien.Alien _alien;

        public override void Initialize(AgentAI<AlienState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _alien = (Alien.Alien)agent;
            _alien.TargetFinder.OnTargetChanged += OnTargetChanged;
        }

        private void OnDestroy()
        {
            _alien.TargetFinder.OnTargetChanged -= OnTargetChanged;
        }

        private void OnTargetChanged()
        {
            Agent.StateMachineCompo.ChangeState(AlienState.Follow);
        }

        public override void EnterState()
        {
            base.EnterState();
            _alien.TargetFinder.Target = null;
            Agent.MovementCompo.Move(Vector3.zero, _alien.MoveSpeed);
        }
    }
}