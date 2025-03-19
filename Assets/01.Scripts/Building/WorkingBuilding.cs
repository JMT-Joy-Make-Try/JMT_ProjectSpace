using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Building
{
    public class WorkingBuilding : BuildingBase
    {
        [field:SerializeField] public SerializedDictionary<CreateItemSO, bool> CreateItemList {  get; private set; }
        public override void Build(Vector3 position, Transform parent)
        {
            
        }
    }
}