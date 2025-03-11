using AYellowpaper.SerializedCollections;
using JMT.Building;
using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Manager
{
    public class BuildingManager : MonoSingleton<BuildingManager>
    {
        public BuildingDataSO CurrentBuilding;
        [SerializeField] private List<BuildingDataSO> buildingDatas;

        public List<BuildingDataSO> GetDictionary() => buildingDatas;
    }
}