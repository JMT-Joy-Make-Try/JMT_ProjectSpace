using JMT.Core.Manager;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Building
{
    public class HospitalBuilding : BuildingBase
    {
        [field: SerializeField] public float HealingTime = 5f;

        private void Start()
        {
            BuildingManager.Instance.HospitalBuilding = this;
        }
        
        
    }
}