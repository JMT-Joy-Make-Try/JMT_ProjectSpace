using UnityEngine;

namespace JMT.Agent.Alien
{
    public class Alien : AgentAI<AlienState>
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public LayerMask WhatIsPlayer { get; private set; }
        [field: SerializeField] public LayerMask WhatIsBuilding { get; private set; }
        [field: SerializeField] public LayerMask WhatIsNPC { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            StateMachineCompo.ChangeState(AlienState.Idle);
        }
    }
}