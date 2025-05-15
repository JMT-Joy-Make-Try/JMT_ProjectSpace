using JMT.Agent.NPC;

namespace JMT.Agent.State
{
    public class NPCIdleState : State<NPCState>
    {
        public override void UpdateState()
        {
            Agent.StateMachineCompo.ChangeState(NPCState.Move);
        }
    }
}