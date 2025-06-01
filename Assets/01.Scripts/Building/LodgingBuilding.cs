using JMT.Agent;
using JMT.Core.Manager;
using UnityEngine;

namespace JMT.Building
{
    public class LodgingBuilding : FacilityBuilding
    {
        [SerializeField] private int _npcCount;
        protected override void HandleCompleteEvent()
        {
            base.HandleCompleteEvent();
            BuildingManager.Instance.LodgingBuildings.Add(this);
            AgentManager.Instance.AddMaxNpcCount(_npcCount);
        }
    }
}