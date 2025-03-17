using AYellowpaper.SerializedCollections;
using JMT.Building;
using JMT.Planets.Tile.Items;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Building
{
    public enum BuildingCategory
    {
        ResourceBuilding, // 자원 건물
        FacilityBuilding, // 설비 건물
        ConvenientBuilding, // 편의 건물
        DefenseBuilding, // 방어 건물
    }
    [CreateAssetMenu(menuName = "SO/Data/BuildingDataSO")]
    public class BuildingDataSO : ScriptableObject
    {
        public BuildingBase prefab;
        public BuildingCategory category;
        public string buildingName;
        public SerializedDictionary<ItemType, int> needItems;
        public TimeData buildTime;
    }
}
