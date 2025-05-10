using DG.Tweening;
using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Building.Component;
using JMT.Core.Tool;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Object;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
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
        
        private IEnumerator _workCoroutine;
        
        private bool _isAnimating;

        public void InventoryAdd()
        {
            if (_isAnimating) return;
            if (_currentProductionAmount <= 0) return;
            GameUIManager.Instance.InventoryCompo.AddItem(ProductionItem, _currentProductionAmount);
            

            StartCoroutine(AnimateItem());
        }

        private IEnumerator AnimateItem()
        {
            _isAnimating = true;
            for (int i = 0; i < _currentProductionAmount; i++)
            {
                var item = PoolingManager.Instance.Pop(PoolingType.Item) as ItemObject;
                item.transform.position = transform.position + Vector3.up * 10f;
                item.IsCollectable = false;
                Vector3 targetPos = AgentManager.Instance.Player.transform.position;
                
                item.transform.DOMove(targetPos, 2f).OnComplete(() =>
                {
                    PoolingManager.Instance.Push(item);
                }).SetEase(Ease.OutBounce);
                item.SetItemType(ProductionItem);
                
                yield return new WaitForSeconds(0.2f);
            }
            
            _currentProductionAmount = 0;
            GetBuildingComponent<BuildingData>().CurrentItems.Clear();
            _isAnimating = false;
        }

        public override void Work()
        {
            base.Work();
            _workCoroutine = WorkCoroutine();
            StartCoroutine(_workCoroutine);
        }
        
        public override void StopWork()
        {
            base.StopWork();
            if (_workCoroutine != null)
            {
                StopCoroutine(_workCoroutine);
                _workCoroutine = null;
            }
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
                var buildingData = GetBuildingComponent<BuildingData>();
                if (buildingData.CurrentItems.Contains(ProductionItem.ItemType))
                {
                    buildingData.CurrentItems.Find(x => x.Item1 == ProductionItem.ItemType).Item2 += GetBuildingComponent<BuildingLevel>().CurLevel;;
                }
                else
                {
                    buildingData.CurrentItems.Add(
                        new SerializeTuple<ItemType, int>(ProductionItem.ItemType, _currentProductionAmount));
                }

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

            if (transform.position.IsNear(AgentManager.Instance.Player.transform.position, 10f))
            {
                InventoryAdd();
            }
        }
    }
}