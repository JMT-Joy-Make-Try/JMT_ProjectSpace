using JMT.Agent;
using UnityEngine;

namespace JMT.Building
{
    public class WorkshopBuilding : BuildingBase
    {
        public override void Build(Vector3 position, Transform parent)
        {
            var npc = AgentManager.Instance.GetAgent();
            _currentNpc.Add(npc);
        }
    }
}