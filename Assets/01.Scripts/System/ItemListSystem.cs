using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT
{
    public class ItemListSystem : MonoSingleton<ItemListSystem>
    {
        [SerializeField] private List<ItemSO> itemList;

        public List<ItemSO> ItemList => itemList;

        public ItemSO GetItemSO(ItemType type)
            => itemList.FirstOrDefault(item => item.ItemType == type);
    }
}
