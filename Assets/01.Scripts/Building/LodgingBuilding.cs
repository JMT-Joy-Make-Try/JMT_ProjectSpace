using JMT.Agent;
using JMT.Core.Manager;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.UISystem;
using System.Collections;
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
            
            Debug.Log(GameUIManager.Instance.ResourceCompo.MaxNpcValue);
            for (int i = 0; i < GameUIManager.Instance.ResourceCompo.MaxNpcValue; i++)
            {
                var agent = AgentManager.Instance.AddNpc();
            }

            PoolingManager.Instance.ResetPool(PoolingType.Agent_NPC);
        }
    }
}