using JMT.Agent.NPC;
using JMT.Building;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCWork : MonoBehaviour, INPCComponent
    {
        public NPCAgent Agent { get; private set; }
        [field: SerializeField] public BuildingBase CurrentWorkingBuilding { get; private set; }
        [field: SerializeField] public PlanetTile CurrentWorkingPlanetTile { get; private set; }
        public bool IsWorking { get; private set; }
        
        public void Initialize(NPCAgent agent)
        {
            Agent = agent;
        }
        
        
        public void SetBuilding(BuildingBase building)
        {
            CurrentWorkingBuilding = building;
        }
    }
}