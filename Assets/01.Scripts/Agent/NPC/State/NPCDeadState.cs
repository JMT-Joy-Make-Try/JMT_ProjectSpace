namespace JMT.Agent.State
{
    public class NPCDeadState : State<NPCState>
    {
        public override void OnAnimationEnd()
        {
            Agent.gameObject.SetActive(false);
        }
    }
}