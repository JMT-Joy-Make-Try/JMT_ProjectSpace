using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.Building
{
    public class DustCollector : BuildingBase
    {

        public override void Build(Vector3 position, Transform parent)
        {
        }

        public override void Work()
        {
            // foreach (var item in increaseItems)
            // {
            //     InventoryManager.Instance.AddItem(item.Key, item.Value);
            // }
        }
    }
}