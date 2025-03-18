using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public class WorkingBuilding : BuildingBase
    {
        [SerializeField] private List<ItemType> _itemType;
        public override void Build(Vector3 position, Transform parent)
        {
            
        }
    }
}