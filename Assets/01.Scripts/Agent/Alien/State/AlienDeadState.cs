using JMT.Agent.Alien;
using JMT.Core.Tool.PoolManager.Core;

namespace JMT.Agent.State
{
    public class AlienDeadState : State<AlienState>
    {
        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            PoolingManager.Instance.Push(Agent);
        }
    }
}