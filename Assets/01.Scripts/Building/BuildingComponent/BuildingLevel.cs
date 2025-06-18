using System;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingLevel : MonoBehaviour, IBuildingComponent
    {
        public event Action<int> OnLevelChanged;
        public BuildingBase Building { get; private set; }
        [SerializeField] private BuildingDataSO data;
        [SerializeField] private int maxLevel = 3;
        [SerializeField] private bool _isLevelUpgradable = true;
        private int _curLevel;
        
        public int CurLevel
        {
            get => _curLevel;
            set
            {
                if (_curLevel == value) return;
                _curLevel = value;
                OnLevelChanged?.Invoke(_curLevel - 1);
            }
        }
        
        
        public void Upgrade()
        {
            if (!_isLevelUpgradable)
            {
                Debug.LogWarning("Building is not upgradable.");
                return;
            }
            if (_curLevel >= maxLevel)
            {
                Debug.LogWarning("Building has reached the maximum level.");
                return;
            }
            _curLevel++;
            OnLevelChanged?.Invoke(_curLevel - 1);
        }

        public void Init(BuildingBase building)
        {
            Building = building;
            _curLevel = 1; // Initialize to level 1
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Upgrade();
            }
        }
    }
}