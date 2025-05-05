using AYellowpaper.SerializedCollections;
using JMT.Item;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.UISystem.Inventory
{
    public class InventoryModel
    {
        [SerializeField] private InventorySO inventorySO;

        public InventoryModel(InventorySO so)
        {
            inventorySO = so;
        }

        public void AddItem(ItemSO item, int increaseCount)
        {
            if (!inventorySO.ItemDictionary.ContainsKey(item)) inventorySO.ItemDictionary.Add(item, increaseCount);
            else inventorySO.ItemDictionary[item] += increaseCount;
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
            if (!inventorySO.ItemDictionary.ContainsKey(item))
                return;
            else
            {
                inventorySO.ItemDictionary[item] -= decreaseCount;
                if (inventorySO.ItemDictionary[item] <= 0)
                    inventorySO.ItemDictionary.Remove(item);
            }
        }

        public void RemoveItem(ItemType item, int decreaseCount)
        {
            var itemSO = inventorySO.ItemDictionary.FirstOrDefault(s => (s.Key).ItemType == item).Key;
            if (itemSO == null)
            {
                Debug.LogError($"Item of type {item} not found in inventory.");
                return;
            }
            if (!inventorySO.ItemDictionary.ContainsKey(itemSO))
                return;
            else
            {
                inventorySO.ItemDictionary[itemSO] -= decreaseCount;
                if (inventorySO.ItemDictionary[itemSO] <= 0)
                    inventorySO.ItemDictionary.Remove(itemSO);
            }
        }

        public bool CalculateItem(SerializedDictionary<ItemSO, int> needItems)
        {
            var pairs = needItems.ToList();
            for (int i = 0; i < pairs.Count; ++i)
            {
                var pair = pairs[i];
                if (!inventorySO.ItemDictionary.ContainsKey(pair.Key) || inventorySO.ItemDictionary[pair.Key] < pair.Value)
                {
                    return false;
                }
            }

            for (int i = 0; i < pairs.Count; ++i)
            {
                var pair = pairs[i];
                inventorySO.ItemDictionary[pair.Key] -= pair.Value;
            }
            return true;
        }

        public List<KeyValuePair<ItemSO, int>> SelectCategory(InventoryCategory? category = null)
        {
            var dic = GameUIManager.Instance.InventoryCompo.InventorySO.ItemDictionary;

            var pairs = dic.ToList();
            if (category != null)
                pairs = CategorySystem.FilteringCategory(pairs, category, x => x.Key);

            return pairs;
        }
    }
}
