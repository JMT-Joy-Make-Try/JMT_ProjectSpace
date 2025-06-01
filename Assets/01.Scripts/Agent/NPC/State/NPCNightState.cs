using JMT.Agent.NPC;
using JMT.Building;
using JMT.Building.Component;
using JMT.Core.Manager;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCNightState : State<NPCState>
    {
        private NPCAgent _npcAgent;
        private NPCMovement _npcMovement;

        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _npcAgent = agent as NPCAgent;
            _npcMovement = agent.MovementCompo as NPCMovement;
        }

        public override void EnterState()
        {
            base.EnterState();
            
            var lodging = FindLodgingBuilding();
            _npcMovement.SetBuilding(lodging);
            _npcMovement.Move(lodging.GetBuildingComponent<BuildingNPC>().WorkPosition.position, 10);
        }

        private LodgingBuilding FindLodgingBuilding()
        {
            var lodgingBuildings = BuildingManager.Instance.LodgingBuildings;
            return lodgingBuildings[Random.Range(0, lodgingBuildings.Count)];
        }
    }
}