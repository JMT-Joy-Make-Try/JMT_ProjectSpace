using AYellowpaper.SerializedCollections;
using JMT.Core.Tool;
using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public class WorkingBuilding : BuildingBase
    {
        [field:SerializeField] public SerializedDictionary<ItemType, bool> CreateItemList {  get; private set; }
        public override void Build(Vector3 position, Transform parent)
        {
            
        }
    }
}