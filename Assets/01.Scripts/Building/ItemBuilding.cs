using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using JMT.UISystem;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public class ItemBuilding : BuildingBase
    {
        public ItemBuildingData data;

        protected override void Awake()
        {
            base.Awake();
            data.Init(this);
        }
    }
}