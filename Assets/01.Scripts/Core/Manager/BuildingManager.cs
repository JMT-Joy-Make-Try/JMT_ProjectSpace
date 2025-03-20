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

        protected override void Awake()
        {
            base.Awake();
            var basebuilding = Instantiate(_baseBuilding, new Vector3(0, -257, 0), Quaternion.identity);
            basebuilding.Build(new Vector3(0, -257, 0), transform);
        }
    }
}