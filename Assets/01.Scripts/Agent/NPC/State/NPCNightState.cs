using JMT.Agent.NPC;
using JMT.Building;
using JMT.Building.Component;

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

        public async override void EnterState()
        {
            base.EnterState();
            
            var lodging = await _npcAgent.BuildingFinderCompo.FindNearbyBuilding<LodgingBuilding>();
            _npcMovement.SetBuilding(lodging);
            _npcMovement.Move(lodging.GetBuildingComponent<BuildingNPC>().WorkPosition.position, 10);
        }
    }
}