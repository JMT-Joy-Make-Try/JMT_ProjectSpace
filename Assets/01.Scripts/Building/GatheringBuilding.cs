using JMT.Agent;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.Building
{
    public class GatheringBuilding : BuildingBase
    {
        [SerializeField] private ItemType _productionItem;
        [SerializeField] private int _productionAmount;
        
        private int _currentProductionAmount;

        [Space] [Header("Debug")] [SerializeField]
        private NPCAgent _npcAgent;

        protected override void Awake()
        {
            base.Awake();
            OnClick += InventoryAdd;
        }

        private void InventoryAdd()
        {
            InventoryManager.Instance.AddItem(_productionItem, _currentProductionAmount);
            _currentProductionAmount = 0;
        }

        public override void Build(Vector3 position, Transform parent)
        {
            transform.position = position;
        }

        public override void Work()
        {
            base.Work();
            if (!_isWorking)
            {
                _isWorking = true;
                // 애들 가져오는 애니메이션 해줘야함
            }
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                AddNpc(_npcAgent);
            }
        }
#endif


        // 이건 애들이 가져오면 호출이 될거임
        public void AddItem(int itemAmount)
        {
            _currentProductionAmount += itemAmount;
        }
    }
}