using JMT.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JMT.Core;

namespace JMT.NightSummary.Component
{
    // 설치되어있는 건물
    [Serializable]
    public class BuildingModule : IResetable
    {
        [SerializeField] private List<BuildingModuleData> _buildingDataList = new();

        public void AddBuilding(BuildingType buildingType, int level, int count)
        {
            var existingBuilding = _buildingDataList.FirstOrDefault(b => b.BuildingType == buildingType && b.Level == level);
            if (existingBuilding != null)
            {
                existingBuilding.Count += count;
            }
            else
            {
                _buildingDataList.Add(new BuildingModuleData(buildingType, level, count));
            }
        }

        public List<BuildingModuleData> GetBuildings()
        {
            return _buildingDataList;
        }

        public int GeBuildingsCount()
        {
            int result = 0;
            for (int i = 0; i < _buildingDataList.Count; i++)
                result += _buildingDataList[i].Count;
            return result;
        }

        public string GetBuildingSummary(BuildingModuleData data)
        {
            return $"{data.BuildingType} Lv.{data.Level} X{data.Count}";
        }

        public void Reset()
        {
            _buildingDataList.Clear();
        }
    }

    [Serializable]
    public class BuildingModuleData
    {
        public BuildingType BuildingType;
        public int Level;
        public int Count;
        
        public BuildingModuleData(BuildingType buildingType, int level, int count)
        {
            BuildingType = buildingType;
            Level = level;
            Count = count;
        }
    }
}