using JMT.Agent.NPC;
using JMT.Core.Manager;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Agent
{
    public class AgentManager : MonoSingleton<AgentManager>
    {
        [field: SerializeField] public List<NPCAgent> UnemployedAgents { get; private set; } = new();
        [field: SerializeField] public PlayerCharacter.Player Player { get; private set; }

        public NPCAgent AddNpc()
        {
            if (BuildingManager.Instance.LodgingBuildings.Count <= 0)
            {
                GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("숙소가 필요합니다.");
                return null;
            }
            NPCAgent agent = PoolingManager.Instance.Pop(PoolingType.Agent_NPC) as NPCAgent;
            
            agent.SetAgentType(AgentType.Base);
            
            return agent;
        }
        
        public void SpawnNpc(Vector3 position, Quaternion rotation)
        {
            NPCAgent agent = PoolingManager.Instance.Pop(PoolingType.Agent_NPC) as NPCAgent;
            
            agent.transform.position = position;
            agent.transform.rotation = rotation;
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
            GameUIManager.Instance.ResourceCompo.AddNpc(1);
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

        public void AddMaxNpcCount(int count)
        {
            GameUIManager.Instance.ResourceCompo.AddMaxNpc(count);
        }

        public bool IsBuildingNotEnough()
        {
            return GameUIManager.Instance.ResourceCompo.MaxNpcValue <=
                   GameUIManager.Instance.ResourceCompo.CurrentNpcValue;
        }
        
        public bool IsContainAgent(NPCAgent agent)
        {
            return UnemployedAgents.Contains(agent);
        }
        
    }
}