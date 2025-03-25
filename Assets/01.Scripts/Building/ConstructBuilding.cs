using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using JMT.UISystem;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public class ConstructBuilding : BuildingBase
    {
        [field:SerializeField] public SerializedDictionary<CreateItemSO, bool> CreateItemList {  get; private set; }
        protected List<CreateItemSO> _createItems;
        public override void Build(Vector3 position, Transform parent)
        {
            
        }
    }
}