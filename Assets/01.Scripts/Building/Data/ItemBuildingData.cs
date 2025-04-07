using JMT.Planets.Tile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    [Serializable]
    public class ItemBuildingData : BaseData
    {
        public List<CreateItemSO> CreateItemList;
        
        public CreateItemSO GetFirstCreateItem()
        {
            if (CreateItemList.Count > 0)
            {
                return CreateItemList[0];
            }
            return null;
        }

        public override void AddWork(BuildingWork work)
        {
            base.AddWork(work);
            foreach (var createItem in CreateItemList)
            {
                foreach (var item in createItem.NeedItemList)
                {
                    InventoryManager.Instance.RemoveItem(item.Key, item.Value);
                }
            }
        }
        
        public override void RemoveWork()
        {
            base.RemoveWork();
            foreach (var createItem in CreateItemList)
            {
                InventoryManager.Instance.AddItem(createItem.ResultItem, 1);
            }
        }
    }
}
