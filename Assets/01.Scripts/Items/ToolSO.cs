using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "Tool", menuName = "SO/Data/ToolSO")]
    public class ToolSO : ScriptableObject
    {
        public Sprite Icon;
        public InventoryCategory Category;
        public string ItemName;
        public string ItemDescription;
        // public ItemType ItemType;
    }
}
