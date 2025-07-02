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
        public override void Interaction()
        {
            Destroy(transform.GetChild(0).gameObject);
            for (int i = 0; i < itemCount; i++)
            {
                var item = PoolingManager.Instance.Pop(PoolingType.Item) as ItemObject;
                Debug.Log(transform.position);
                item.transform.position = transform.position + Vector3.up * 5f;
                item.IsCollectable = true;
                item.SetItem(itemType);
            }
            TileManager.Instance.CurrentTile.RemoveInteraction();
            TileManager.Instance.CurrentTile.AddInteraction<NoneInteraction>();
        }
        
        public void SetItem(ItemSO item, int count)
        {
            itemType = item;
            itemCount = count;
        }
    }
}
