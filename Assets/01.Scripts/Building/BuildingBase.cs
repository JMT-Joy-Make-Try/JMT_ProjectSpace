using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Core.Tool;
using JMT.Object;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public abstract class BuildingBase : TouchableObject
    {
        [field: SerializeField] public int NpcCount { get; protected set; }
        [Space]
        [Header("Decrease Item")]
        [SerializeField] protected SerializedDictionary<ItemType, int> decreaseItems;
        
        [SerializeField] protected List<NPCAgent> _currentNpc;
        
        protected event Action OnStartWorking;
        protected bool _isWorking;
        
        [SerializeField] protected AgentType _agentType;
        public abstract void Build(Vector3 position, Transform parent);

        protected virtual void Awake()
        {
            _currentNpc = new List<NPCAgent>();
        }

        protected virtual void Start()
        {
            OnStartWorking += Work;
            
        }

        protected virtual void OnDestroy()
        {
            OnStartWorking -= Work;
        }

        public virtual void Work()
        {
            if (_isWorking)
            {
                return;
            }
            _isWorking = true;
        }

        public virtual void AddNpc(NPCAgent agent)
        {
            _currentNpc.Add(agent);
            agent.SetAgentType(_agentType);
            //if (!_isWorking)
            //    OnStartWorking?.Invoke();
        }
        
        public virtual void Upgrade()
        {
        }

      
    }
}
