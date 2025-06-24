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
        }
    }
}
