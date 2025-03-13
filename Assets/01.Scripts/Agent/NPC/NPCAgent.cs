using AYellowpaper.SerializedCollections;
using JMT.Agent.State;
using JMT.Building;
using JMT.Core.Tool;
using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCAgent : AgentAI<NPCState>
    {
        [SerializeField] private SerializedDictionary<ItemType, int> needItems;
        [field: SerializeField] public NPCData Data { get; set; }
        public bool IsActive { get; private set; }
        public BuildingBase CurrentWorkingBuilding { get; set; }
        public bool IsWorking { get; private set; }

        [Space]
        [SerializeField] private List<Range> _healthRange;
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _workSpeed;
        
        
        protected override void Awake()
        {
            base.Awake();
            StateMachineCompo.ChangeState(NPCState.Idle);
        }

        public void TakeItem(ItemType itemType, int count)
        {
            if (needItems.Count == 0)
                ActiveAgent();
            if (!needItems.ContainsKey(itemType))
                return;
            needItems[itemType] -= count;
            if (needItems[itemType] <= 0)
                needItems.Remove(itemType);
        }
        
        private void ActiveAgent()
        {
            IsActive = true;
        }
        
        protected void SetSpeed()
        {
            _workSpeed = MathExtension.GetPercentageValue(_workSpeed, GetPercent(Data.Health));
            _moveSpeed = MathExtension.GetPercentageValue(_moveSpeed, GetPercent(Data.Health));

            if (_workSpeed.IsZero() && _moveSpeed.IsZero()) // 나중에 일도 포함시켜야함
            {
                StateMachineCompo.ChangeState(NPCState.Dead);
            }
        }

        protected int GetPercent(float health)
        {
            int healthPercent = Mathf.RoundToInt(health * 100 / Data.MaxHealth);
            return healthPercent.GetRangeValue(_healthRange);
        }
        
        
    }
}
