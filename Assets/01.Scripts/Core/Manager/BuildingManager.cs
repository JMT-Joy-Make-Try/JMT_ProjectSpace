using JMT.Building;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Manager
{
    public class BuildingManager : MonoSingleton<BuildingManager>
    {
        [SerializeField] private BaseBuilding _baseBuilding;
        public BuildingDataSO CurrentBuilding;
        [SerializeField] private List<BuildingDataSO> buildingDatas;
        
        [field: SerializeField] public HospitalBuilding HospitalBuilding { get; set; }
        [field: SerializeField] public OxygenBuilding OxygenBuilding { get; set; }
        [field: SerializeField] public LodgingBuilding LodgingBuilding { get; set; }

        public BaseBuilding BaseBuilding => _baseBuilding;
        public List<BuildingDataSO> GetDictionary() => buildingDatas;
    }
}