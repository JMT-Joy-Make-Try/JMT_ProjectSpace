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
    }
}
