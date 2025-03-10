using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<ItemType, int> itemDictionary = new();

        public void AddItem(ItemType type, int increaseCount)
        {
            if(!itemDictionary.ContainsKey(type))
                itemDictionary.Add(type, increaseCount);
            else itemDictionary[type] += increaseCount;
        }
    }
}
