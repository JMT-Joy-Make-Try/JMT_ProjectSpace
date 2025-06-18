using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Core.Manager;
using JMT.Item;
using JMT.PlayerCharacter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace JMT.Planets.Tile
{
    public class PreBuildInteraction : TileInteraction
    {
        public event Action<List<PreBuildItemData>> OnChangedDataEvent;
        [SerializeField] private SerializedDictionary<ItemSO, int> _requiredItems = new();
        private bool _isCompleteReceive = false;
        private bool _isBuildComplete = false;
        [SerializeField] private List<PreBuildItemData> _preBuildItemDatas = new();

        private PVCBuilding pvc;
        
        public List<PreBuildItemData> PreBuildItemDatas => _preBuildItemDatas;
        
        private Coroutine _prebuildCoroutine;


        private void OnDestroy()
        {
            OnChangedDataEvent -= pvc.PVCUI.SetNeedItemUI;
        }

        public override void Interaction()
        {
            base.Interaction();
            var inventory = AgentManager.Instance.Player.InventoryCompo;
            if (!_requiredItems.ContainsKey(inventory.PlayerInventoryData.item))
            {
                Debug.LogError("Required item not found in inventory.");
                return;
            }

            if (_prebuildCoroutine == null)
            {
                _prebuildCoroutine = StartCoroutine(DelayUpdatePreBuildItem(inventory));
            }
        }

        private IEnumerator DelayUpdatePreBuildItem(PlayerInventory inventory)
        {
            yield return null;
            var item = inventory.RemoveItem(inventory.PlayerInventoryData.item, 3, false);
            for (int i = 0; i < 3; i++)
            {
                _requiredItems[item] -= 1;
                if (_requiredItems[item] <= 0)
                {
                    _requiredItems.Remove(item);
                }
                var preBuildItem = FindPreBuildItemCount(item);
                if (preBuildItem != null)
                {
                    preBuildItem.CurItemCount++;
                }
                
                
                OnChangedDataEvent?.Invoke(_preBuildItemDatas);
                Debug.Log("PreBuildInteraction: Item used for building.");
                Debug.Log(i);
                
                yield return new WaitForSeconds(0.5f);
                Debug.Log("TimeScale"+Time.timeScale);
            }
            
            
            _isCompleteReceive = _requiredItems.Count <= 0;
            
            if (_isCompleteReceive && !_isBuildComplete)
            {
                _isBuildComplete = true;
                planetTile.Build(BuildingManager.Instance.CurrentBuilding, pvc);
            }
            
            _prebuildCoroutine = null;
            inventory.ResetItem();
        }

        public PreBuildItemData FindPreBuildItemCount(ItemSO item)
        {
            var preBuildItem = _preBuildItemDatas.FirstOrDefault(x => x.Item == item);
            return preBuildItem;
        }

        
        public void SetRequiredItems(Dictionary<ItemSO, int> requiredItems)
        {
            _requiredItems = new SerializedDictionary<ItemSO, int>(requiredItems);
            _isCompleteReceive = false;
            _isBuildComplete = false;
            
            _preBuildItemDatas.Clear();
            foreach (var item in _requiredItems)
            {
                _preBuildItemDatas.Add(new PreBuildItemData(item.Key, 0, item.Value));
            }
            
            pvc = Instantiate(BuildingManager.Instance.CurrentBuilding.PVCPrefab, transform);
            OnChangedDataEvent += pvc.PVCUI.SetNeedItemUI;
            pvc.SetVisualActive(false);
            
            if (_requiredItems.Count <= 0)
            {
                StartCoroutine(DelayBuild());
            }
        }

        private IEnumerator DelayBuild()
        {
            yield return new WaitForSeconds(0.1f);
            _isBuildComplete = true;
            planetTile.Build(BuildingManager.Instance.CurrentBuilding, pvc);
        }
    }

    [System.Serializable]
    public class PreBuildItemData
    {
        public ItemSO Item;
        public int CurItemCount;
        public int MaxItemCount;

        public PreBuildItemData(ItemSO item, int curItemCount, int maxItemCount)
        {
            Item = item;
            CurItemCount = curItemCount;
            MaxItemCount = maxItemCount;
        }
    }
}