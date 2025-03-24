using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Agent.NPC;
using JMT.CameraSystem;
using JMT.Core;
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
    public abstract class BuildingBase : TouchableObject, IDamageable
    {
        [field: SerializeField] public int NpcCount { get; protected set; }
        [Space] [SerializeField] protected List<NPCAgent> _currentNpc;
        [SerializeField] protected SerializeQueue<SerializeTuple<ItemType, int>> CurrentItems;
        private BuildingDataSO buildingData;
        [field: SerializeField] public int Health { get; }
        protected int _curHealth;

        protected event Action OnBuildingBroken;
        protected bool _isWorking;

        [SerializeField] protected AgentType _agentType;

        private int _curLevel;
        public abstract void Build(Vector3 position, Transform parent);

        protected virtual void Awake()
        {
            _currentNpc = new List<NPCAgent>();
        }

        protected virtual void Start()
        {
            OnClick += HandleClick;
        }

        private void HandleClick()
        {
            //CameraManager.Instance.ZoomCamera(1.2f, 1f);
            //CameraManager.Instance.LookCamera(transform, 1f);
        }

        protected virtual void OnDestroy()
        {
            OnClick -= HandleClick;
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
            agent.SetBuilding(this);
            if (!_isWorking)
            {
                Work();
            }
        }

        public virtual void Upgrade()
        {
            _curLevel++;
        }

        protected virtual void SetItem(ItemType type, int amount)
        {
            CurrentItems.Enqueue(new SerializeTuple<ItemType, int>(type, amount));
        }

        public void SetBuildingData(BuildingDataSO data)
        {
            buildingData = data;
        }

        public BuildingDataSO BuildingData => buildingData;

        public void InitHealth()
        {
            _curHealth = Health;
        }

        public void TakeDamage(int damage)
        {
            _curHealth -= damage;
            if (_curHealth <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            OnBuildingBroken?.Invoke();
        }
    }
}