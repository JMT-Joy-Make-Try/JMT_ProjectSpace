using AYellowpaper.SerializedCollections;
using JMT.Building;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.Building
{
    [CreateAssetMenu(menuName = "SO/Data/BuildingDataSO")]
    public class BuildingDataSO : ScriptableObject
    {
        public BuildingBase prefab;
        public string name;
        public SerializedDictionary<ItemType, int> needItems;
    }
}
