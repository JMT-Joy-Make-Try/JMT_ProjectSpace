using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using JMT.UISystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        [SerializeField] private SerializedDictionary<InventorySO, int> itemDictionary = new();
        public SerializedDictionary<InventorySO, int> ItemDictionary => itemDictionary;

        public void AddItem(ItemSO item, int increaseCount)
        {
            if(!itemDictionary.ContainsKey(item))
                itemDictionary.Add(item, increaseCount);
            else itemDictionary[item] += increaseCount;
            UIManager.Instance.GetUI.GetItem(item, increaseCount);
        }

        /*public void AddItem(ItemType item, int increaseCount)
        {
            var itemSO = itemDictionary.FirstOrDefault(s => ((ItemSO)s.Key).ItemType == item).Key;
            if (itemSO == null)
            {
                Debug.LogError($"Item of type {item} not found in inventory.");
                return;
            }
            if (!itemDictionary.ContainsKey(itemSO))
                itemDictionary.Add(itemSO, increaseCount);
            else itemDictionary[itemSO] += increaseCount;
        }*/
        
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
        
        public void RemoveItem(ItemType item, int decreaseCount)
        {
            var itemSO = itemDictionary.FirstOrDefault(s => ((ItemSO)s.Key).ItemType == item).Key;
            if (itemSO == null)
            {
                Debug.LogError($"Item of type {item} not found in inventory.");
                return;
            }
            if (!itemDictionary.ContainsKey(itemSO))
                return;
            else
            {
                itemDictionary[itemSO] -= decreaseCount;
                if (itemDictionary[itemSO] <= 0)
                    itemDictionary.Remove(itemSO);
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
