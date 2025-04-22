using System;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingLevel : MonoBehaviour, IBuildingComponent
    {
        public event Action<int> OnLevelChanged;
        public BuildingBase Building { get; private set; }
        [SerializeField] private BuildingDataSO data;
        private int _curLevel;
        
        public int CurLevel
        {
            get => _curLevel;
            set
            {
                if (_curLevel == value) return;
                _curLevel = value;
                OnLevelChanged?.Invoke(_curLevel);
            }
        }
        
        
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