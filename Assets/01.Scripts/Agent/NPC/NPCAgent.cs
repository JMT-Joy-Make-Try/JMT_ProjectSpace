using JMT.Agent.State;

namespace JMT.Agent
{
    public class NPCAgent : AgentAI<NPCState>
    {
        
        protected override void Awake()
        {
            base.Awake();
            StateMachineCompo.ChangeState(NPCState.Idle);
        }
    }
}
