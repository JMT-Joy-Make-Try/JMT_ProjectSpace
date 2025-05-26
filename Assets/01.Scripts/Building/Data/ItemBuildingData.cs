using JMT.Building.Component;
using JMT.Core.Tool;
using JMT.Item;
using System;
using UnityEngine;

namespace JMT.Building
{
    [Serializable]
    public class ItemBuildingData : BaseData
    {
        public SizeLimitQueue<CreateItemSO> CreateItemList = new(4);
        
        public event Action<ItemSO, int> OnItemCreate;

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
                OnItemCreate?.Invoke(createItem.ResultItem, 1);
            }
        }

        public override void RemoveWork(bool isAddItem = true)
        {
            base.RemoveWork(isAddItem);
            if (!isAddItem) return;
            foreach (var createItem in CreateItemList)
            {
                if (_buildingBase == null)
                {
                    Debug.LogError("BuildingBase is null");
                    return;
                }
                _buildingBase.GetBuildingComponent<BuildingData>().SetItem(createItem.ResultItem.ItemType, 1);
            }
        }

        public void RemoveWork(BuildingWork work)
        {
            Works.Remove(work);
        }
    }
}
