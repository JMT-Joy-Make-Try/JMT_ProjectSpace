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

        public List<BuildingDataSO> GetDictionary() => buildingDatas;

        private void Start()
        {
            var basebuilding = Instantiate(_baseBuilding, Vector3.zero, Quaternion.identity);
            basebuilding.Build(Vector3.zero, transform);
        }
    }
}