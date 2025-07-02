using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using JMT.Core;

namespace JMT.NightSummary.Component
{
    // 획득한 자원
    [Serializable]
    public class CollectItemModule : IResetable
    {
        [SerializeField] private List<CollectItemData> _collectItemDataList = new();

        
        
        public void AddItem(ItemType item, int count)
        {
            var existingItem = _collectItemDataList.Find(data => data.ItemType == item);
            if (existingItem != null)
            {
                existingItem.Count += count;
            }
            else
            {
                _collectItemDataList.Add(new CollectItemData(item, count));
            }
        }
        
        public List<CollectItemData> GetCollectedItems()
        {
            return _collectItemDataList;
        }

        public int GetCollectedItemsCount()
        {
            int result = 0;
            for(int i = 0; i <  _collectItemDataList.Count; i++)
                result += _collectItemDataList[i].Count;
            return result;
        }
        
        public string GetItemSummary(CollectItemData data)
        {
            return $"{data.ItemType} X{data.Count}";
        }

        public void Reset()
        {
            _collectItemDataList.Clear();
        }
    }

    [Serializable]
    public class CollectItemData
    {
        public ItemType ItemType;
        public int Count;

        public CollectItemData(ItemType itemType, int count)
        {
            ItemType = itemType;
            Count = count;
        }
    }
}