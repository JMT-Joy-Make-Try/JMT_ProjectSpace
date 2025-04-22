using JMT.Agent.NPC;
using JMT.Building.Component;
using JMT.Item;
using JMT.Planets.Tile;
using JMT.UISystem;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Building
{
    public class GatheringBuilding : BuildingBase
    {
        [field: SerializeField] public ItemSO ProductionItem { get; private set; }
        [SerializeField] private float _productionTime;
        [SerializeField] private int _maxProductionAmount;
        private int _currentProductionAmount;
        private float _productGauge;
        

        [Space] [Header("Debug")] [SerializeField]
        private NPCAgent _npcAgent;

        public void InventoryAdd()
        {
            GameUIManager.Instance.InventoryCompo.AddItem(ProductionItem, _currentProductionAmount);
            _currentProductionAmount = 0;
        }

        public override void Work()
        {
            base.Work();
            StartCoroutine(WorkCoroutine());
        }

        private IEnumerator WorkCoroutine()
        {
            var ws = new WaitForSeconds(_productionTime);
            while (_isWorking)
            {
                if (_currentProductionAmount >= _maxProductionAmount)
                {
                    yield return ws;
                    continue;
                }
                _currentProductionAmount += GetBuildingComponent<BuildingLevel>().CurLevel;
                InventoryAdd();
                // 대충 연료 소모
                yield return ws;
            }
        }

        private void Update()
        {
            if (_isWorking)
            {
                _productGauge += Time.deltaTime / _productionTime;
                if (_productGauge >= 1f)
                {
                    _productGauge = 0f;
                }
            }
        }
    }
}