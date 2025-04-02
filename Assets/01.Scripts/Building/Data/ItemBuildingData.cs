using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public class ItemBuildingData : BaseData
    {
        [SerializeField] private List<CreateItemSO> makeItemList = new();

        public List<CreateItemSO> MakeItemList => makeItemList;
    }
}
