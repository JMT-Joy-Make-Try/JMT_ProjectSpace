using UnityEngine;
using AYellowpaper.SerializedCollections;
using EditorAttributes;
using JMT.Agent;
using JMT.Item;

namespace JMT.Building
{
    public class VillageBuilding : BuildingBase
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> _needItems;
        [SerializeField] private int _npcCount;
        
        [SerializeField] private ItemSO _item;
        
        public SerializedDictionary<ItemSO, int> NeedItems => _needItems;
        [Button]
        private void Test()
        {
            GiveItem(_item, 1);
        }
        
        public void GiveItem(ItemSO item, int amount)
        {
            if (!_needItems.ContainsKey(item))
            {
                Debug.LogError($"Item {item} not found in need items.");
                return;
            }
            _needItems[item] -= amount;
            if (_needItems[item] <= 0)
            {
                _needItems.Remove(item);
            }
            if (_needItems.Count <= 0)
            {
                for (int i = 0; i < _npcCount; i++)
                {
                    AgentManager.Instance.SpawnAgent(transform.position);
                }
            }
        }
    }
}