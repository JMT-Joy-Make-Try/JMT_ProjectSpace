using JMT.Agent.NPC;

namespace JMT.Agent.State
{
    public class NPCIdleState : State<NPCState>
    {
        public override void UpdateState()
        {
            if ((_agent as NPCAgent).IsActive)
            {
                _agent.StateMachineCompo.ChangeState(NPCState.Move);
            }
        }
    }
}