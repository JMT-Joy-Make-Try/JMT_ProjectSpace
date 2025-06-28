using JMT.Building;
using JMT.Building.Component;
using JMT.Core.Tool;
using JMT.NightSummary;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Manager
{
    public class BuildingManager : MonoSingleton<BuildingManager>
    {
        [SerializeField] private List<BuildingDataSO> buildingDatas;
        [SerializeField] private BaseBuilding _baseBuilding;
        
        [field: SerializeField] public List<HospitalBuilding> HospitalBuildings { get; private set; } = new List<HospitalBuilding>();
        [field: SerializeField] public List<OxygenBuilding> OxygenBuildings { get; private set; } = new List<OxygenBuilding>();
        [field: SerializeField] public List<LodgingBuilding> LodgingBuildings { get; private set; } = new List<LodgingBuilding>();
        
        
        public BuildingDataSO CurrentBuilding; 
        public BaseBuilding BaseBuilding => _baseBuilding;
        public List<BuildingBase> Buildings => _buildings;
        public List<BuildingDataSO> GetDictionary() => buildingDatas;
        
        private List<BuildingBase> _buildings = new List<BuildingBase>();
        private List<float> _defaultFuelAmount = new List<float>();


        public void AddBuildingDataSO(BuildingDataSO buildingData)
        {
            if (buildingData == null) return;
            if (buildingDatas.Contains(buildingData)) return;
            buildingDatas.Add(buildingData);
        }
        
        public void AddBuilding(BuildingBase building)
        {
            if (building == null) return;
            _buildings.Add(building);
            _defaultFuelAmount.Add(building.GetBuildingComponent<BuildingFuel>().FuelAmount);
        }
        
        public void RemoveBuilding(BuildingBase building)
        {
            if (building == null) return;
            _buildings.Remove(building);
            _defaultFuelAmount.Remove(building.GetBuildingComponent<BuildingFuel>().FuelAmount);
        }

        public void SetFuelDecreaseValuePercent(float percent)
        {
            foreach (var building in _buildings)
            {
                var fuel = building.GetBuildingComponent<BuildingFuel>();
                fuel.FuelAmount = fuel.FuelAmount.GetPercentageValue(percent);
            }
        }
        
        public void ResetFuel()
        {
            for (int i = 0; i < _buildings.Count; i++)
            {
                _buildings[i].GetBuildingComponent<BuildingFuel>().FuelAmount = _defaultFuelAmount[i];
            }
        }
    }
}