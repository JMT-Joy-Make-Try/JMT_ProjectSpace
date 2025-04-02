using UnityEngine;

namespace JMT.Agent.NPC
{
    public class NPCOxygen : MonoBehaviour
    {
        private NPCAgent _npcAgent;
        public void Init(NPCAgent agent)
        {
            _npcAgent = agent;
        }
        public void AddOxygen(float amount)
        {
            _npcAgent.Data.OxygenAmount += amount;
            if (_npcAgent.Data.OxygenAmount < 0)
            {
                _npcAgent.WorkSpeed -= 1;
            }
        }
    }
}