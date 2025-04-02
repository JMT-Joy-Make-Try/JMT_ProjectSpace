using JMT.Agent.NPC;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
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


        public void SpawnAgent(Vector3 position)
        {
            var obj = PoolingManager.Instance.Pop(PoolingType.Agent_NPC);
            obj.ObjectPrefab.transform.position = position;
        }

        public NPCAgent GetAgent()
        {
            if (UnemployedAgents.Count == 0)
            {
                Debug.LogWarning("No unemployed agents");
                return null;
            }
            NPCAgent agent = UnemployedAgents[0];
            if (agent == null)
            {
                Debug.LogWarning("No unemployed agents");
                return null;
            }
            return agent;
        }

        private void SetUnemployedAgents()
        {
            UnemployedAgents = FindObjectsByType<NPCAgent>(FindObjectsSortMode.None)
                .ToList().FindAll(s => s.AgentType == AgentType.Base);
        }

        public void RegisterAgent(NPCAgent agent)
        {
            if (agent == null) Debug.LogWarning("Agent is null");
            if (UnemployedAgents == null) UnemployedAgents = new List<NPCAgent>();
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