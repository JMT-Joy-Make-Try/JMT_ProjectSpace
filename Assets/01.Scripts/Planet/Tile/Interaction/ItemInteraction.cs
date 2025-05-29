using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Object;
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
            if (TileManager.Instance.CurrentTile.Fog.IsFogActive) return;
            Destroy(transform.GetChild(0).gameObject);
            for (int i = 0; i < itemCount; i++)
            {
                var item = PoolingManager.Instance.Pop(PoolingType.Item) as ItemObject;
                item.transform.position = transform.position + Vector3.up * 5f;
                item.IsCollectable = true;
                item.SetItemType(itemType);
            }
            //GameUIManager.Instance.InventoryCompo.AddItem(itemType, itemCount);
            TileManager.Instance.CurrentTile.RemoveInteraction();
            TileManager.Instance.CurrentTile.AddInteraction<NoneInteraction>();
            //UIManager.Instance.ItemUI.OpenUI();
        }
        
        public void SetItem(ItemSO item, int count)
        {
            itemType = item;
            itemCount = count;
        }
    }
}
