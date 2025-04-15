using JMT.Agent.NPC;
using JMT.Item;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT.Building
{
    public class GatheringBuilding : BuildingBase
    {
        [field: SerializeField] public ItemSO ProductionItem { get; private set; }
        [SerializeField] private int _productionAmount;
        
        private int _currentProductionAmount;

        [Space] [Header("Debug")] [SerializeField]
        private NPCAgent _npcAgent;

        protected override void Awake()
        {
            base.Awake();
            //OnClick += InventoryAdd;
        }

        private void InventoryAdd()
        {
            InventoryManager.Instance.AddItem(ProductionItem, _currentProductionAmount);
            _currentProductionAmount = 0;
        }

        public override void Work()
        {
            base.Work();
        }


        // 이건 애들이 가져오면 호출이 될거임
        public void AddItem(int itemAmount)
        {
            _currentProductionAmount += itemAmount;
        }
    }
}