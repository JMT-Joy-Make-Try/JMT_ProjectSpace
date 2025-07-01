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

    [CreateAssetMenu(menuName = "SO/Data/Items/ItemSO")]
    public class ItemSO : ScriptableObject, ICategorizable, ICellDisplayData
    {
        //public Sprite Icon;
        public ItemType ItemType;
        public InventoryCategory Category;
        public string ItemName;
        public string ItemDescription;
        public ItemData ItemData;

        Enum ICategorizable.Category => Category;
        public Sprite DisplayIcon => ItemData.Icon;
        public string DisplayName => ItemName;
        public bool IsUsable => ItemType is
                        ItemType.LiquidFuel or
                        ItemType.RefinedFuel or
                        ItemType.StaleOxygen or 
                        ItemType.OxygenCylinder;
        // TODO: 현재 코스튬을 장비로 처리할지 확정되지 않아 임시로 구현함. 확정된 후 적절하게 수정해야함
        // 코스튬 기획 끝나면 수정 예정
        public bool IsTakeable => Category != InventoryCategory.Tool;
    }
}
