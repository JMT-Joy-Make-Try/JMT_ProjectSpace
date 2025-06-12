using JMT.Agent;
using JMT.Building.Component;
using JMT.Core.Manager;
using UnityEngine;

namespace JMT.Building
{
    public class LodgingBuilding : FacilityBuilding
    {
        [SerializeField] private int _npcCount;
        
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
            BuildingManager.Instance.LodgingBuildings.Add(this);
            AgentManager.Instance.AddMaxNpcCount(_npcCount);
        }
    }
}