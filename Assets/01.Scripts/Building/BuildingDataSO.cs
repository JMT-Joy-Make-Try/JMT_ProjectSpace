using AYellowpaper.SerializedCollections;
using JMT.Building;
using JMT.Item;
using JMT.Planets.Tile.Items;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Building
{
    public enum BuildingCategory
    {
        ItemBuilding, // 자원 건물
        AttackBuilding, // 공격 건물
        FacilityBuilding, // 설비 건물
        MoveBuilding, // 이동 건물
    }
    [CreateAssetMenu(menuName = "SO/Data/BuildingDataSO")]
    public class BuildingDataSO : ScriptableObject
    {
        public BuildingBase prefab;
        public BuildingCategory category;
        public string buildingName;
        [TextArea(4, 10)] public string buildingDescription;
        public SerializedDictionary<ItemSO, int> needItems;
        public TimeData buildTime;
        public float useFuelPerSecond;
    }
}
