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
    public abstract class BuildingBase : MonoBehaviour, IDamageable
    {
        [SerializeField] protected Animator buildingAnimator;
        [field: SerializeField] public int NpcCount { get; protected set; }
        [field: SerializeField] public Transform WorkPosition { get; protected set; }
        [Space] [SerializeField] protected List<NPCAgent> _currentNpc;
        [SerializeField] protected SerializeQueue<SerializeTuple<ItemType, int>> CurrentItems;
        private BuildingDataSO buildingData;
        [field: SerializeField] public int Health { get; protected set; }
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

        public virtual void Work()
        {
            if (_isWorking)
            {
                return;
            }

            _isWorking = true;
            SetAnimation(_isWorking);   
        }

        public virtual void AddNpc(NPCAgent agent)
        {
            _currentNpc.Add(agent);
            agent.SetBuilding(this);
            agent.SetAgentType(_agentType);
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

        public void InitStat()
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

        protected void SetAnimation(bool isWalking)
        {
            if (buildingAnimator == null)
            {
                Debug.LogWarning("No animator attached to building");
                return;
            }
            buildingAnimator.SetBool("IsWalking", isWalking);
        }
    }
}