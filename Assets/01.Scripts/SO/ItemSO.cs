using JMT.Object;
using JMT.Planets.Tile.Items;
using System;
using UnityEngine;

namespace JMT.Item
{
    public enum InventoryCategory
    {
        Item, //자원
        Tool, //도구
        Costume, //복장
    }

    [CreateAssetMenu(menuName = "SO/Data/ItemSO")]
    public class ItemSO : ScriptableObject, ICategorizable
    {
        //public Sprite Icon;
        public ItemType ItemType;
        public InventoryCategory Category;
        public string ItemName;
        public string ItemDescription;
        public ItemData ItemData;

        Enum ICategorizable.Category => Category;
    }
}
