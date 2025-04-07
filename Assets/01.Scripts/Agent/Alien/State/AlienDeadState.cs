using DG.Tweening;
using JMT.Agent.Alien;
using JMT.Core.Tool.PoolManager.Core;

namespace JMT.Agent.State
{
    public class AlienDeadState : State<AlienState>
    {
        private Alien.Alien _alien;

        public override void Initialize(AgentAI<AlienState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _alien = agent as Alien.Alien;
        }

        public override void EnterState()
        {
            base.EnterState();
            Agent.MovementCompo.Stop(true);
            _alien.AlienRenderer.material.DOFloat(1f, "_DissolveValue", 1f);
        }

        public override void OnAnimationEnd()
        {
            PoolingManager.Instance.Push(Agent);
        }
    }
}