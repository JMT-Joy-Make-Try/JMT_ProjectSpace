using AYellowpaper.SerializedCollections;
using JMT.Item;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.UISystem.Inventory
{
    public class InventoryModel
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> itemDictionary = new();
        public SerializedDictionary<ItemSO, int> ItemDictionary => itemDictionary;

        public void AddItem(ItemSO item, int increaseCount)
        {
            if (!itemDictionary.ContainsKey(item))
                itemDictionary.Add(item, increaseCount);
            else itemDictionary[item] += increaseCount;
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
            if (!itemDictionary.ContainsKey(item))
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
            var itemSO = itemDictionary.FirstOrDefault(s => (s.Key).ItemType == item).Key;
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
            for (int i = 0; i < pairs.Count; ++i)
            {
                var pair = pairs[i];
                if (!itemDictionary.ContainsKey(pair.Key) || itemDictionary[pair.Key] < pair.Value)
                {
                    return false;
                }
            }

            for (int i = 0; i < pairs.Count; ++i)
            {
                var pair = pairs[i];
                itemDictionary[pair.Key] -= pair.Value;
            }
            return true;
        }

        public List<KeyValuePair<ItemSO, int>> SelectCategory(InventoryCategory? category = null)
        {
            var dic = InventoryManager.Instance.ItemDictionary;

            var pairs = dic.ToList();
            if (category != null)
                pairs = CategorySystem.FilteringCategory(pairs, category);

            return pairs;
        }
    }
}
