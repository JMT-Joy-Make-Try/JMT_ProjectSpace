using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Core.Manager;
using JMT.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JMT.Planets.Tile
{
    public class PreBuildInteraction : TileInteraction, IItemReceivable
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> _requiredItems = new();
        private bool _isCompleteReceive = false;
        private bool _isBuildComplete = false;

        public override void Interaction()
        {
            base.Interaction();
            if (_isCompleteReceive && !_isBuildComplete)
            {
                planetTile.Build(BuildingManager.Instance.CurrentBuilding, BuildingManager.Instance.CurrentBuilding.PVCPrefab);
            }
        }

        public bool ReceiveItem(ItemSO item, int amount)
        {
            if (_requiredItems.Count <= 0)
            {
                _isCompleteReceive = true;
                return false;
            }
            if (_requiredItems.ContainsKey(item))
            {
                _requiredItems[item] -= amount;
                if (_requiredItems[item] <= 0)
                {
                    _requiredItems.Remove(item);
                    _isCompleteReceive = true;
                }
                return true;
            }
            
            return false;
        }
        
        public void SetRequiredItems(Dictionary<ItemSO, int> requiredItems)
        {
            _requiredItems = requiredItems as SerializedDictionary<ItemSO, int>;
            _isCompleteReceive = false;
            _isBuildComplete = false;
            
            if (_requiredItems.Count <= 0)
            {
                StartCoroutine(DelayBuild());
            }
        }

        private IEnumerator DelayBuild()
        {
            yield return new WaitForSeconds(0.1f);
            _isBuildComplete = true;
            planetTile.Build(BuildingManager.Instance.CurrentBuilding, BuildingManager.Instance.CurrentBuilding.PVCPrefab);
        }
    }
}