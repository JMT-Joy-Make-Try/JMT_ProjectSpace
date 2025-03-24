using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "Item", menuName = "SO/Data/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public Sprite Icon;
        public string ItemName;
        public ItemType ItemType;
    }
}
