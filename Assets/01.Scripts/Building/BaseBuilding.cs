using JMT.Building.Component;
using JMT.Core;
using JMT.Item;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Building
{
    public class BaseBuilding : BuildingBase, IItemReceivable
    {
        [SerializeField] private Transform visual, brokenVisual;
        
        private BuildingBuilder _buildingBuilder;
        
        protected override void Awake()
        {
            base.Awake();
            _buildingBuilder = GetBuildingComponent<BuildingBuilder>();
        }

        protected override void AddEvents()
        {
            base.AddEvents();
            _buildingBuilder.OnCompleteEvent += HandleCompleteEvent;
        }
        
        protected override void RemoveEvents()
        {
            base.RemoveEvents();
            _buildingBuilder.OnCompleteEvent -= HandleCompleteEvent;
        }

        private void HandleCompleteEvent()
        {
            FixStation();
            GetBuildingComponent<BuildingAnimator>().SetAnimation(true);
        }


        public void FixStation()
        {
            visual.gameObject.SetActive(true);
            brokenVisual.gameObject.SetActive(false);
        }

        public bool ReceiveItem(ItemSO item, int amount)
        {
            if (!_buildingBuilder.IsBuildingComplete) return false;
            BuildingUIManager.Instance.StorageCompo.AddItem(item, amount);
            return true;
        }
    }
}