using AYellowpaper.SerializedCollections;
using JMT.Agent.State;
using JMT.Building;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCAgent : AgentAI<NPCState>
    {
        [SerializeField] private SerializedDictionary<ItemType, int> needItems;
        public bool IsActive { get; private set; }
        public BuildingBase CurrentWorkingBuilding { get; set; }
        public bool IsWalking { get; private set; }
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
        
        
    }
}
