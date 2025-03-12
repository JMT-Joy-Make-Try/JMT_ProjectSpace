using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class ItemInteraction : TileInteraction
    {
        /*InventoryManager.Instance.AddItem(itemType, itemCount);
            base.Interaction(tile);*/
        public override void Interaction(PlanetTile tile)
        {
            UIManager.Instance.ItemUI.OpenUI();
            base.Interaction(tile);
        }
    }
}
