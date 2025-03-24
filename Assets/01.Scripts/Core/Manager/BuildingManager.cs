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
            _baseBuilding.Build(new Vector3(0, 0, 0), transform);
        }
    }
}