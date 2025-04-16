using AYellowpaper.SerializedCollections;
using JMT.Item;
using UnityEngine;

namespace JMT.Building
{
    public enum BuildingCategory
    {
        ItemBuilding, // 자원 건물
        DefenseBuilding, // 공격 건물
        FacilityBuilding, // 설비 건물
        MoveBuilding, // 이동 건물
    }
    [CreateAssetMenu(menuName = "SO/Data/BuildingDataSO")]
    public class BuildingDataSO : ScriptableObject
    {
        public Sprite Icon;
        public BuildingBase Prefab;
        public BuildingCategory Category;
        public string BuildingName;
        [TextArea(4, 10)] public string BuildingDescription;
        public SerializedDictionary<ItemSO, int> NeedItems;
        public TimeData BuildTime;
        public float UseFuelPerSecond;
    }
}
