using JMT.Building;
using JMT.Core.Tool;
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
        
        private List<BuildingBase> _buildings = new List<BuildingBase>();
        private List<float> _defaultFuelAmount = new List<float>();
        
        public List<BuildingBase> Buildings => _buildings;
        
        public void AddBuilding(BuildingBase building)
        {
            if (building == null) return;
            _buildings.Add(building);
            _defaultFuelAmount.Add(building.FuelAmount);
        }
        
        public void RemoveBuilding(BuildingBase building)
        {
            if (building == null) return;
            _buildings.Remove(building);
            _defaultFuelAmount.Remove(building.FuelAmount);
        }

        public void SetFuelDecreaseValuePercent(float percent)
        {
            foreach (var building in _buildings)
            {
                building.FuelAmount = building.FuelAmount.GetPercentageValue(percent);
            }
        }
        
        public void ResetFuel()
        {
            for (int i = 0; i < _buildings.Count; i++)
            {
                _buildings[i].FuelAmount = _defaultFuelAmount[i];
            }
        }
    }
}