using JMT.Agent.NPC;
using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCWorkState : State<NPCState>
    {
        private NPCAgent npcAgent;
        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            npcAgent = agent as NPCAgent;
            npcAgent.OnTypeChanged += HandleTypeChanged;
        }

        private void HandleTypeChanged(AgentType obj)
        {
            
        }

        public override void EnterState()
        {
            base.EnterState();
        }
    }
}