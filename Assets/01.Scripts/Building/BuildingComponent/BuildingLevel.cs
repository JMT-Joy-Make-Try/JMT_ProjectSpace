using System;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingLevel : MonoBehaviour, IBuildingComponent
    {
        public event Action<int> OnLevelChanged;
        public BuildingBase Building { get; private set; }
        private int _curLevel;
        
        
        public void Upgrade()
        {
            _curLevel++;
            OnLevelChanged?.Invoke(_curLevel);
        }

        public void Init(BuildingBase building)
        {
            Building = building;
        }
    }
}