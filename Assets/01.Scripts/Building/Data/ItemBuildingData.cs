using JMT.Core.Tool;
using JMT.Planets.Tile;
using System;
using UnityEngine;

namespace JMT.Building
{
    [Serializable]
    public class ItemBuildingData : BaseData
    {
        public SizeLimitQueue<CreateItemSO> CreateItemList = new(4);
        
        public CreateItemSO GetFirstCreateItem()
        {
            if (CreateItemList.Count > 0)
            {
                return CreateItemList.Peek();
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
                if (_buildingBase == null)
                {
                    Debug.LogError("BuildingBase is null");
                    return;
                }
                _buildingBase.SetItem(createItem.ResultItem.ItemType, 1);
            }
        }
    }
}
