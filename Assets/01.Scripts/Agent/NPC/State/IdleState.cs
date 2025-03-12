namespace JMT.Agent.State
{
    public class IdleState : State<NPCState>
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