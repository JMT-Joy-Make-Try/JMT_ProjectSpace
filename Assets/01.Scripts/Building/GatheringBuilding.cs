using DG.Tweening;
using JMT.Agent;
using JMT.Building.Component;
using JMT.Core.Tool;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Object;
using JMT.Planets.Tile.Items;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Building
{
    public class GatheringBuilding : BuildingBase
    {
        public event Action OnAddItemEvent;
        
        [field: SerializeField] public ItemSO ProductionItem { get; private set; }
        [SerializeField] private float _productionTime;
        [SerializeField] private int _maxProductionAmount;
        
        private int _currentProductionAmount;
        private float _productGauge;
        private bool _isAnimating;
        
        private IEnumerator _workCoroutine;
        private BuildingWorker _worker;

        protected override void Awake()
        {
            base.Awake();
            _worker = GetBuildingComponent<BuildingWorker>();
        }
        
        protected override void AddEvents()
        {
            base.AddEvents();
            _worker.OnWorkingEvent += HandleWork;
        }
        
        protected override void RemoveEvents()
        {
            base.RemoveEvents();
            _worker.OnWorkingEvent -= HandleWork;
        }

        private void HandleWork(bool isWorking)
        {
            if (isWorking)
            {
                Work();
                GetBuildingComponent<BuildingBuilder>().AutoSoundPlayer.PlaySound();
            }
            else
            {
                StopWork();
                GetBuildingComponent<BuildingBuilder>().AutoSoundPlayer.StopSound();
            }
        }

        public void InventoryAdd()
        {
            if (_isAnimating) return;
            if (_currentProductionAmount <= 0) return;
            bool isEquipSuccess = AgentManager.Instance.Player.InventoryCompo.AddItem(ProductionItem, 3);
            if (!isEquipSuccess)
            {
                Debug.LogWarning("Inventory is full or item type mismatch.");
                return;
            }
            OnAddItemEvent?.Invoke();

            StartCoroutine(AnimateItem());
        }

        private IEnumerator AnimateItem()
        {
            _isAnimating = true;
            for (int i = 0; i < 3; i++)
            {
                var item = PoolingManager.Instance.Pop(PoolingType.Item) as ItemObject;
                item.transform.position = transform.position + Vector3.up * 10f;
                item.IsCollectable = false;
                Vector3 targetPos = AgentManager.Instance.Player.transform.position;
                
                item.transform.DOMove(targetPos, 2f).OnComplete(() =>
                {
                    PoolingManager.Instance.Push(item);
                }).SetEase(Ease.OutBounce);
                item.SetItem(ProductionItem);
                
                yield return new WaitForSeconds(0.2f);
            }
            
            _currentProductionAmount = 0;
            GetBuildingComponent<BuildingData>().CurrentItems.Clear();
            _isAnimating = false;
        }

        public void Work()
        {
            _workCoroutine = WorkCoroutine();
            StartCoroutine(_workCoroutine);
        }
        
        public void StopWork()
        {
            if (_workCoroutine != null)
            {
                StopCoroutine(_workCoroutine);
                _workCoroutine = null;
            }
        }

        private IEnumerator WorkCoroutine()
        {
            var ws = new WaitForSeconds(_productionTime);
            while (_worker.IsWorking)
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
            if (_worker.IsWorking)
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