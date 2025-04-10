using UnityEngine;
using AYellowpaper.SerializedCollections;
using EditorAttributes;
using JMT.Agent;

namespace JMT.Building
{
    public class VillageBuilding : BuildingBase
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> _needItems;
        [SerializeField] private int _npcCount;
        
        [SerializeField] private ItemSO _item;
        [Button]
        private void Test()
        {
            GiveItem(_item, 1);
        }
        
        public void GiveItem(ItemSO item, int amount)
        {
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