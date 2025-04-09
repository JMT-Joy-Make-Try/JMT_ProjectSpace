using UnityEngine;
using AYellowpaper.SerializedCollections;
using JMT.Agent;

namespace JMT.Building
{
    public class VillageBuilding : BuildingBase
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> _needItems;
        [SerializeField] private int _npcCount;

        public void GiveItem(ItemSO item, int amount)
        {
            _needItems[item] -= amount;
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