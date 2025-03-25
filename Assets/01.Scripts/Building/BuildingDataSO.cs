using AYellowpaper.SerializedCollections;
using JMT.Building;
using JMT.Planets.Tile.Items;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Building
{
    public enum BuildingCategory
    {
        GatheringBuilding, // 채집 건물
        ConstructBuilding, // 제작 건물
        FacilityBuilding, // 설비 건물
        MovementBuilding, // 이동 건물
    }
    [CreateAssetMenu(menuName = "SO/Data/BuildingDataSO")]
    public class BuildingDataSO : ScriptableObject
    {
        public BuildingBase prefab;
        public BuildingCategory category;
        public string buildingName;
        public SerializedDictionary<ItemSO, int> needItems;
        public TimeData buildTime;
    }
}
