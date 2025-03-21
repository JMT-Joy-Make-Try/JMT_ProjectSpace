namespace JMT.Agent.Alien
{
    public class Alien : AgentAI<AlienState>
    {
        protected override void Awake()
        {
            base.Awake();
            StateMachineCompo.ChangeState(AlienState.Idle);
        }
    }
}