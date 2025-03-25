using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> itemDictionary = new();
        public SerializedDictionary<ItemSO, int> ItemDictionary => itemDictionary;

        public void AddItem(ItemSO item, int increaseCount)
        {
            if(!itemDictionary.ContainsKey(item))
                itemDictionary.Add(item, increaseCount);
            else itemDictionary[item] += increaseCount;
        }
        
        public void RemoveItem(ItemSO item, int decreaseCount)
        {
            if(!itemDictionary.ContainsKey(item))
                return;
            else
            {
                itemDictionary[item] -= decreaseCount;
                if (itemDictionary[item] <= 0)
                    itemDictionary.Remove(item);
            }
        }

        public bool CalculateItem(SerializedDictionary<ItemSO, int> needItems)
        {
            var pairs = needItems.ToList();
            for(int i = 0; i < pairs.Count; ++i)
            {
                KeyValuePair<ItemSO, int> pair = pairs[i];
                if (!itemDictionary.ContainsKey(pair.Key)) return false;
                if (itemDictionary[pair.Key] < pair.Value) return false;
            }

            for (int i = 0; i < pairs.Count; ++i)
            {
                KeyValuePair<ItemSO, int> pair = pairs[i];
                itemDictionary[pair.Key] -= pair.Value;
            }
            return true;
        }
    }
}
