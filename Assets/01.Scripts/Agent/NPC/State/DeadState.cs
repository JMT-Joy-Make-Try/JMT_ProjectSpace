namespace JMT.Agent.State
{
    public class DeadState : State<NPCState>
    {
        public override void OnAnimationEnd()
        {
            _agent.gameObject.SetActive(false);
        }
    }
}