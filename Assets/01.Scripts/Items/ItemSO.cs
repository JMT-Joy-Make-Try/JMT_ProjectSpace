using JMT.Object;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT
{
    public enum InventoryCategory
    {
        Item, //자원
        Tool, //도구
        Costume, //복장
    }

    [CreateAssetMenu(fileName = "Item", menuName = "SO/Data/ItemSO")]
    public class ItemSO : InventorySO
    {
        public ItemType ItemType;
        public ItemData ItemData;
    }
}
