using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "Costume", menuName = "SO/Data/CostumeSO")]
    public class CostumeSO : ScriptableObject
    {
        public Sprite Icon;
        public InventoryCategory Category;
        public string ItemName;
        public string ItemDescription;
        // public ItemType ItemType;
    }
}
