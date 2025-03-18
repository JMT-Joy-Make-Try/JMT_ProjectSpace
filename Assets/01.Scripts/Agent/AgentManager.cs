using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Agent
{
    public class AgentManager : MonoSingleton<AgentManager>
    {
        [field: SerializeField] public int MaxAgentCount { get; private set; }
        [field: SerializeField] public NPCAgent NPCAgent { get; private set; }
        [field: SerializeField] public List<NPCAgent> UnemployedAgents { get; private set; }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SpawnAgent(new Vector3(0, 100, 0));
            }
        }

        public void SpawnAgent(Vector3 position)
        {
            NPCAgent.Spawn(position);
        }

        public NPCAgent GetAgent()
        {
            NPCAgent agent = UnemployedAgents[0];
            UnemployedAgents.RemoveAt(0);
            return agent;
        }

        private void SetUnemployedAgents()
        {
            UnemployedAgents = FindObjectsByType<NPCAgent>(FindObjectsSortMode.None)
                .ToList().FindAll(s => s.AgentType == AgentType.Base);
        }

        public void RegisterAgent(NPCAgent agent)
        {
            if (UnemployedAgents.Contains(agent))
            {
                Debug.LogWarning($"Agent {agent.name} is already unemployed");
                return;
            }
            UnemployedAgents.Add(agent);
        }

        public void UnregisterAgent(NPCAgent agent)
        {
            if (!UnemployedAgents.Contains(agent))
            {
                Debug.LogWarning($"Agent {agent.name} is not unemployed");
                return;
            }
            UnemployedAgents.Remove(agent);
        }
    }
}