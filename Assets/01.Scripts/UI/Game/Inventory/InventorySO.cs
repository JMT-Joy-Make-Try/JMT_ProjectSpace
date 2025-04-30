using AYellowpaper.SerializedCollections;
using JMT.Item;
using UnityEngine;

namespace JMT.UISystem.Inventory
{
    [CreateAssetMenu(fileName = "InventorySO", menuName = "SO/Data/InventorySO")]
    public class InventorySO : ScriptableObject
    {
        public SerializedDictionary<ItemSO, int> ItemDictionary;
    }
}
