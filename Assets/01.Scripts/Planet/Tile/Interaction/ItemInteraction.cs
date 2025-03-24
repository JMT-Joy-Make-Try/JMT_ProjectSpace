using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class ItemInteraction : TileInteraction
    {
        /*InventoryManager.Instance.AddItem(itemType, itemCount);
            base.Interaction(tile);*/
        public override void Interaction()
        {
            Destroy(TileManager.Instance.CurrentTile.transform.GetChild(0).gameObject);
            InventoryManager.Instance.AddItem(itemType, itemCount);
            TileManager.Instance.CurrentTile.RemoveInteraction();
            TileManager.Instance.CurrentTile.AddInteraction<NoneInteraction>();
            //UIManager.Instance.ItemUI.OpenUI();
        }
    }
}
