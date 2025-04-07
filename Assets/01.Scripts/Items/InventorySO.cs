using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT
{
    public class InventorySO : ScriptableObject
    {
        public Sprite Icon;
        public InventoryCategory Category;
        public string ItemName;
        public string ItemDescription;
        public ItemType ItemType;
    }
}
