using JMT.Agent.NPC;
using JMT.Core;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCStat : AgentHealth, INPCComponent, IOxygen
    {
        public NPCAgent Agent { get; private set; }

        private float _health; // NPC의 건강
        public int Oxygen { get; private set; } // NPC의 산소량
        private float _satisfaction; // NPC의 만족도
        
        public void Initialize(NPCAgent agent)
        {
            Agent = agent;
        }

        public void AddOxygen(int value)
        {
            
        }
    }
}