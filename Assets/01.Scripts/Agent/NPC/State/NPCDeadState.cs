namespace JMT.Agent.State
{
    public class NPCDeadState : State<NPCState>
    {
        public override void OnAnimationEnd()
        {
            _agent.gameObject.SetActive(false);
        }
    }
}