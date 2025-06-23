using JMT.Core.Tool.PoolManager.Core;
using System.Collections.Generic;
using JMT.Core.Tool.PoolManager;
using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using JMT.Agent.NPC;
using JMT.UISystem;
using UnityEngine;
using Random = UnityEngine.Random;
using JMT.NightSummary;
using System;

namespace JMT.Agent
{
    public class AgentManager : MonoSingleton<AgentManager>
    {
        [field: SerializeField] public List<NPCAgent> UnemployedAgents { get; private set; } = new();
        [field: SerializeField] public PlayerCharacter.Player Player { get; private set; }
        [SerializeField] private Trader.Trader _traderPrefab;

        private Trader.Trader _trader;
        private void Start()
        {
            _trader = Instantiate(_traderPrefab, transform);
            _trader.gameObject.SetActive(false);
        }
        
        public void SpawnTrader(Vector3 position, Quaternion rotation)
        {
            if (_trader == null)
            {
                Debug.LogWarning("Trader prefab is not assigned.");
                return;
            }
            _trader.transform.SetParent(null, true);
            _trader.transform.SetPositionAndRotation(position, rotation);
            _trader.gameObject.SetActive(true);
            
        }

        public NPCAgent AddNpc()
        {
            if (BuildingManager.Instance.LodgingBuildings.Count <= 0)
            {
                GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("숙소가 필요합니다.");
                return null;
            }

            NPCAgent agent = null;
            for (int i = 0; i < 10; i++)
            {
                var npc = PoolingManager.Instance.Pop(PoolingType.Agent_NPC) as NPCAgent;
                if (npc != null && !UnemployedAgents.Contains(npc))
                {
                    agent = npc;
                    break;
                }
            }

            if (agent == null)
            {
                Debug.LogWarning("풀에 사용 가능한 에이전트가 없습니다.");
                return null;
            }

            agent.SetAgentType(AgentType.Base);
            return agent;
        }

        public void SpawnNpc(Vector3 position, Quaternion rotation)
        {
            NPCAgent firstAgent = GetAgent();
            NPCAgent agent = PoolingManager.Instance.Pop(firstAgent) as NPCAgent;
            
            agent.transform.position = position;
            agent.transform.rotation = rotation;
        }
        
        public void SpawnNpc(NPCAgent agent)
        {
            var lodgingBuilding = BuildingManager.Instance.LodgingBuildings[Random.Range(0, BuildingManager.Instance.LodgingBuildings.Count)];
            if (lodgingBuilding == null) return;
            var spawnPos = lodgingBuilding.transform.position;

            Instance.SpawnNpc(spawnPos, Quaternion.identity);
            TileManager.Instance.CurrentTile.CurrentBuilding.GetBuildingComponent<BuildingNPC>().AddNpc(agent);
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
            NightSummaryManager.Instance.NPCCollectModule.CollectNPC(agent.StatCompo);
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