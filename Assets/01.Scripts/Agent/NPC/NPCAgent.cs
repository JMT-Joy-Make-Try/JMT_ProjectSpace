using AYellowpaper.SerializedCollections;
using JMT.Agent.State;
using JMT.Building;
using JMT.Core.Tool;
using JMT.Object;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Range = JMT.Core.Tool.Range;

namespace JMT.Agent
{
    public class NPCAgent : AgentAI<NPCState>, ISpawnable
    {
        [Header("Unlock NPC")]
        [SerializeField] private SerializedDictionary<ItemType, int> needItems;
        [field: SerializeField] public NPCData Data { get; set; }
        public bool IsActive { get; private set; }
        
        [Header("Working")]
        [field: SerializeField] public BuildingBase CurrentWorkingBuilding { get; private set; }
        [field: SerializeField] public PlanetTile CurrentWorkingPlanetTile { get; private set; }
        public bool IsWorking { get; private set; }
        public Tuple<ItemType, int> TakeItemTuple { get; private set; }

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
        
        protected override void Awake()
        {
            base.Awake();
            OnTypeChanged += HandleTypeChanged;
            StateMachineCompo.ChangeState(NPCState.Idle);
            ActiveAgent();
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
            WorkSpeed = MathExtension.GetPercentageValue(WorkSpeed, GetPercent(Data.Health));
            MoveSpeed = MathExtension.GetPercentageValue(MoveSpeed, GetPercent(Data.Health));

            if (WorkSpeed.IsZero() && MoveSpeed.IsZero()) // 나중에 일도 포함시켜야함
            {
                StateMachineCompo.ChangeState(NPCState.Dead);
            }
        }

        protected int GetPercent(float health)
        {
            int healthPercent = Mathf.RoundToInt(health * 100 / Data.MaxHealth);
            return healthPercent.GetRangeValue(_healthRange);
        }

        protected void SetBuilding(BuildingBase building)
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

        protected override void Update()
        {
            base.Update();
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     TakeItem(ItemType.LiquidFuel, 1);
            // }
        }

        public void Spawn(Vector3 position)
        {
            Instantiate(this, position, Quaternion.identity);
        }
        
        public void AddOxygen(float amount)
        {
            Data.OxygenAmount += amount;
            if (Data.OxygenAmount < 0)
            {
                WorkSpeed -= 1;
            }
        }
    }
}
