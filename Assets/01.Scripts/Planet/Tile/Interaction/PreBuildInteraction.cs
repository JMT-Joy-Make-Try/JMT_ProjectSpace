using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Core;
using JMT.Core.Manager;
using JMT.Item;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace JMT.Planets.Tile
{
    public class PreBuildInteraction : TileInteraction
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> _requiredItems = new();
        private bool _isCompleteReceive = false;
        private bool _isBuildComplete = false;
        private int _curItemCount = 0, _maxItemCount = 0;
        
        public int CurItemCount => _curItemCount;
        public int MaxItemCount => _maxItemCount;

        private PVCBuilding pvc;

        public override void Interaction()
        {
            base.Interaction();
            var inventory = AgentManager.Instance.Player.InventoryCompo;
            if (!_requiredItems.ContainsKey(inventory.PlayerInventoryData.item)) 
            {
                Debug.LogError("Required item not found in inventory.");
                return;
            }

            _requiredItems[inventory.PlayerInventoryData.item] -= 1;
            _curItemCount++;
            if (_requiredItems[inventory.PlayerInventoryData.item] <= 0)
            {
                _requiredItems.Remove(inventory.PlayerInventoryData.item);
            }
            inventory.RemoveItem();
            _isCompleteReceive = _requiredItems.Count <= 0;
            
            if (_isCompleteReceive && !_isBuildComplete)
            {
                _isBuildComplete = true;
                planetTile.Build(BuildingManager.Instance.CurrentBuilding, pvc);
            }
        }

        
        public void SetRequiredItems(Dictionary<ItemSO, int> requiredItems)
        {
            _requiredItems = new SerializedDictionary<ItemSO, int>(requiredItems);
            _isCompleteReceive = false;
            _isBuildComplete = false;
            _curItemCount = 0;
            _maxItemCount = _requiredItems.First().Value;
            
            pvc = Instantiate(BuildingManager.Instance.CurrentBuilding.PVCPrefab, transform);
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
}