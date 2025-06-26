using JMT.Agent;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class GuestQuest : QuestBase
    {
        public override void Enable()
        {
            base.Enable();
            AgentManager.Instance.SpawnTrader(Tiles[0], new Quaternion(0, 180, 0, 1));
            AgentManager.Instance.Trader.OnInteractEvent += HandleInteractEvent;
        }

        private void OnDestroy()
        {
            if (AgentManager.HasInstance && AgentManager.Instance.Trader != null)
                AgentManager.Instance.Trader.OnInteractEvent -= HandleInteractEvent;
        }

        private void HandleInteractEvent()
        {
            RunQuest(0);
        }
    }
}
