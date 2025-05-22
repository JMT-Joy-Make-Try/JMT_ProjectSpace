using JMT.Agent;
using JMT.Core.Manager;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
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
            
            for (int i = 0; i < 3; i++)
            {
                AgentManager.Instance.AddNpc();
            }
                
            PoolingManager.Instance.ResetPool(PoolingType.Agent_NPC);
        }
    }
}