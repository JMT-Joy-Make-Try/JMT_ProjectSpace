using JMT.Core.Manager;
using System;
using UnityEngine;

namespace JMT.Building
{
    public class LodgingBuilding : FacilityBuilding
    {
        [SerializeField] private int _maxNpcCount;
        
        public int MaxNpcCount => _maxNpcCount;

        private void Start()
        {
            BuildingManager.Instance.LodgingBuilding = this;
        }

        public void AddNpcCount(int npcCount)
        {
            _maxNpcCount += npcCount;
        }
        
        
    }
}