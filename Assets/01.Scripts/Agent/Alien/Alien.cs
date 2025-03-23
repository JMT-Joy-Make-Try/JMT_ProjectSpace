using System;
using UnityEngine;

namespace JMT.Agent.Alien
{
    public class Alien : AgentAI<AlienState>
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public AlienTargetFinder TargetFinder { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            OnDeath += HandleDeath;
            StateMachineCompo.ChangeState(AlienState.Idle);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            StateMachineCompo.ChangeState(AlienState.Dead);
        }

        protected override void Init()
        {
            base.Init();
            StateMachineCompo.ChangeState(AlienState.Idle);
        }
    }
}