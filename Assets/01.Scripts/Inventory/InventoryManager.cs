using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        [SerializeField] private SerializedDictionary<ItemType, int> itemDictionary = new();
        public SerializedDictionary<ItemType, int> GetDictionary() => itemDictionary;

        public void AddItem(ItemType type, int increaseCount)
        {
            if(!itemDictionary.ContainsKey(type))
                itemDictionary.Add(type, increaseCount);
            else itemDictionary[type] += increaseCount;
        }
        
        public void RemoveItem(ItemType type, int decreaseCount)
        {
            if(!itemDictionary.ContainsKey(type))
                return;
            else
            {
                itemDictionary[type] -= decreaseCount;
                if (itemDictionary[type] <= 0)
                    itemDictionary.Remove(type);
            }
        }

        public bool CalculateItem(SerializedDictionary<ItemType, int> needItems)
        {
            var pairs = needItems.ToList();
            for(int i = 0; i < pairs.Count; ++i)
            {
                KeyValuePair<ItemType, int> pair = pairs[i];
                if (!itemDictionary.ContainsKey(pair.Key)) return false;
                if (itemDictionary[pair.Key] < pair.Value) return false;
            }

            for (int i = 0; i < pairs.Count; ++i)
            {
                KeyValuePair<ItemType, int> pair = pairs[i];
                itemDictionary[pair.Key] -= pair.Value;
            }
            return true;
        }
    }
}
