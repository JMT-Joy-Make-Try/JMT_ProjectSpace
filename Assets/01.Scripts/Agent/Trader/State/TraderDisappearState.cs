using JMT.Agent.State;
using JMT.Agent.Trader;

namespace JMT.Agent.State
{
    public class TraderDisappearState : State<TraderStateEnum>
    {
        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            Destroy(Agent.gameObject);
        }
    }
}