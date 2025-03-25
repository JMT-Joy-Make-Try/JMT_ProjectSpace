using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.UISystem
{
    [CreateAssetMenu(fileName = "CreateItemSO", menuName = "SO/Data/CreateItemSO")]
    public class CreateItemSO : ScriptableObject
    {
        public ItemSO ResultItem;
        public SerializedDictionary<ItemSO, int> NeedItemList;
        public int UseFuelCount;
    }
}
