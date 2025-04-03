using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    [Serializable]
    public class ItemBuildingData : BaseData
    {
        public List<CreateItemSO> CreateItemList;
    }
}
