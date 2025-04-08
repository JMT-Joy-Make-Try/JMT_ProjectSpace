using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Agent.State;
using JMT.Core;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public abstract class BuildingBase : MonoBehaviour, IDamageable
    {
        [Header("Animation")] [SerializeField] protected Animator buildingAnimator;
        [SerializeField] protected ParticleSystem buildingParticles;

        [Space(10)] [Header("NPCSetting")] [SerializeField]
        public List<NPCAgent> _currentNpc;

        [field: SerializeField] public Transform WorkPosition { get; protected set; }

        [Space(10)] [Header("Building Data")] [SerializeField]
        protected SerializeQueue<SerializeTuple<ItemType, int>> CurrentItems;

        [field: SerializeField] public int Health { get; protected set; }
        private BuildingDataSO buildingData;
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
        }

        public virtual void RemoveNpc()
        {
            _currentNpc[0].SetAgentType(AgentType.Base);
            _currentNpc[0].ChangeCloth(AgentType.Base);
            _currentNpc[0].SetBuilding(null);
            _currentNpc[0].StateMachineCompo.ChangeState(NPCState.Move);
            _currentNpc.Remove(_currentNpc[0]);
            if (_currentNpc.Count == 0)
            {
                _isWorking = false;
                SetAnimation(_isWorking);
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

        public void TakeDamage(int damage, bool isHeal = false)
        {
            _curHealth += isHeal ? damage : -damage;
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

            if (buildingParticles != null)
            {
                if (isWalking)
                {
                    buildingParticles.Play();
                }
                else
                {
                    buildingParticles.Stop();
                }
            }

            buildingAnimator.SetBool("IsWalking", isWalking);
        }
        
        protected PlanetTile GetPlanetTile()
        {
            return transform.parent.parent.GetComponent<PlanetTile>();
        }
    }
}