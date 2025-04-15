using AYellowpaper.SerializedCollections;
using JMT.Item;
using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "CreateItemSO", menuName = "SO/Data/CreateItemSO")]
    public class CreateItemSO : ScriptableObject
    {
        public ItemSO ResultItem;
        public SerializedDictionary<ItemSO, int> NeedItemList;
        public int UseFuelCount;
        public TimeData CreateTime;
    }
}
