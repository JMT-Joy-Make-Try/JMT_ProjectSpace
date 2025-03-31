using AYellowpaper.SerializedCollections;
using JMT.Agent.State;
using JMT.Building;
using JMT.Core.Tool;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Object;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Range = JMT.Core.Tool.Range;

namespace JMT.Agent.NPC
{
    public class NPCAgent : AgentAI<NPCState>
    {
        [field: SerializeField] public NPCOxygen OxygenCompo { get; private set; }
        [Header("Unlock NPC")]
        [SerializeField] private SerializedDictionary<ItemType, int> needItems;
        [field: SerializeField] public NPCData Data { get; set; }
        public bool IsActive { get; private set; }
        
        [Header("Working")]
        [field: SerializeField] public BuildingBase CurrentWorkingBuilding { get; private set; }
        [field: SerializeField] public PlanetTile CurrentWorkingPlanetTile { get; private set; }
        public bool IsWorking { get; private set; }
        public Tuple<ItemSO, int> TakeItemTuple { get; private set; }

        [Space]
        [SerializeField] private List<Range> _healthRange;
        [field:SerializeField] public int MoveSpeed;
        [field:SerializeField] public int WorkSpeed;
        
        [field:SerializeField] public AgentType AgentType { get; private set; }    
        
        public event Action<AgentType> OnTypeChanged;
        
        public void SetAgentType(AgentType agentType)
        {
            AgentType = agentType;
            OnTypeChanged?.Invoke(agentType);
        }

        protected override void Init()
        {
            base.Init();
            StateMachineCompo.ChangeState(NPCState.Idle);
        }

        protected override void Awake()
        {
            base.Awake();
            OnTypeChanged += HandleTypeChanged;
            OnDeath += HandleDeath;
            
            OxygenCompo = GetComponent<NPCOxygen>();
            StateMachineCompo.ChangeState(NPCState.Idle);
            ActiveAgent();
        }

        protected void OnDestroy()
        {
            OnTypeChanged -= HandleTypeChanged;
            OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            StateMachineCompo.ChangeState(NPCState.Dead);
        }

        private void HandleTypeChanged(AgentType type)
        {
            if (type == AgentType.Base)
            {
                AgentManager.Instance.RegisterAgent(this);
            }
            else
            {
                AgentManager.Instance.UnregisterAgent(this);
            }
            ClothCompo.SetCloth(type);
            AnimatorCompo = ClothCompo.CurrentCloth;
            AnimatorCompo.SetBool(StateMachineCompo.CurrentState.StateName, true);
        }

        private void Start()
        {
            SetAgentType(AgentType.Base);
        }

        public void TakeItem(ItemType itemType, int count)
        {
            if (!needItems.ContainsKey(itemType))
                return;
            needItems[itemType] -= count;
            if (needItems[itemType] <= 0)
            {
                needItems.Remove(itemType);
                if (needItems.Count <= 0)
                {
                    ActiveAgent();
                }
            }
        }
        
        private void ActiveAgent()
        {
            IsActive = true;
        }
        
        protected void SetSpeed()
        {
            WorkSpeed = MathExtension.GetPercentageValue(WorkSpeed, GetPercent(Health));
            MoveSpeed = MathExtension.GetPercentageValue(MoveSpeed, GetPercent(Health));

            if (WorkSpeed.IsZero() && MoveSpeed.IsZero()) // 나중에 일도 포함시켜야함
            {
                StateMachineCompo.ChangeState(NPCState.Dead);
            }
        }

        protected int GetPercent(float health)
        {
            int healthPercent = Mathf.RoundToInt(health * 100 / Health);
            return healthPercent.GetRangeValue(_healthRange);
        }

        public void SetBuilding(BuildingBase building)
        {
            CurrentWorkingBuilding = building;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GatheringBuilding building))
            {
                if (building.ProductionItem != TakeItemTuple.Item1) return;
                
                building.AddItem(TakeItemTuple.Item2);
                TakeItemTuple = null;
            }
        }
    }
}
