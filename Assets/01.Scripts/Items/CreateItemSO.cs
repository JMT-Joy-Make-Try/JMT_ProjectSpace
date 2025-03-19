using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.UISystem
{
    [CreateAssetMenu(fileName = "CreateItemSO", menuName = "SO/Data/CreateItemSO")]
    public class CreateItemSO : ScriptableObject
    {
        public ItemType ResultItem;
        public SerializedDictionary<ItemType, int> NeedItemList;
    }
}
