using AYellowpaper.SerializedCollections;
using JMT.Item;
using JMT.DayTime;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace JMT.Building
{
    public enum BuildingCategory
    {
        ItemBuilding = 0, // 자원 건물
        DefenseBuilding = 1, // 공격 건물
        FacilityBuilding = 2, // 설비 건물
        MoveBuilding = 3, // 이동 건물
    }

    [Serializable]
    public struct BuildingLevelData
    {
        public SerializedDictionary<ItemSO, int> NeedItems;
        public TimeData BuildTime;
        public float UseFuelPerSecond;
    }


    [CreateAssetMenu(menuName = "SO/Data/BuildingDataSO")]
    public class BuildingDataSO : ScriptableObject, ICategorizable
    {
        public Sprite Icon;
        public BuildingBase Prefab;
        public BuildingCategory Category;
        public string BuildingName;
        [TextArea(4, 10)] public string BuildingDescription;

        public List<BuildingLevelData> buildingLevel;
        Enum ICategorizable.Category => Category;
    }
}
