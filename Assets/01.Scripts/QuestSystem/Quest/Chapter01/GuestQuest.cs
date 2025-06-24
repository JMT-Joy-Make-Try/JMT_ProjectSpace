using JMT.Agent;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class GuestQuest : QuestBase
    {
        public override void Enable()
        {
            base.Enable();
            AgentManager.Instance.SpawnTrader(Tiles[0].transform.position, Quaternion.identity);
            AgentManager.Instance.Trader.OnInteractEvent += HandleInteractEvent;
        }

        private void OnDestroy()
        {
            if(AgentManager.Instance.Trader != null)
                AgentManager.Instance.Trader.OnInteractEvent -= HandleInteractEvent;
        }

        private void HandleInteractEvent()
        {
            RunQuest(0);
        }
    }
}
