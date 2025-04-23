using DG.Tweening;
using JMT.Agent.Alien;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Object;
using JMT.Planets.Tile.Items;
using UnityEngine;

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
            //Agent.MovementCompo.Stop(true);
        }

        public override void OnAnimationEnd()
        {
            _alien.AlienRenderer.material.DOFloat(1f, "_DissolveValue", 1f);
            var item = PoolingManager.Instance.Pop(PoolingType.Item) as ItemObject;
            item.transform.position = Agent.transform.position;
            item.SetItemType(ItemListSystem.Instance.GetItemSO(ItemType.OxygenTank));
            PoolingManager.Instance.Push(Agent);
            Debug.Log("Alien Dead");
        }
    }
}