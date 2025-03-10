using JMT.Agent.State;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCAgent : AgentAI<NPCState>
    {
        protected override void Awake()
        {
            base.Awake();
            stateMachine.ChangeState(NPCState.Idle);
        }
        
        protected override void Update()
        {
            base.Update();
        }
    }
}
