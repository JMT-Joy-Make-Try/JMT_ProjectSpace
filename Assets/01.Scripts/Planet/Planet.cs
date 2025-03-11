using System.Collections.Generic;
using JMT.Building;
using JMT.Planets.Tile;
using Unity.AI.Navigation;
using UnityEngine;
using Event = JMT.Planets.Events.Event;

namespace JMT.Planets
{
    public abstract class Planet : MonoBehaviour
    {
        [SerializeField] private List<BuildingBase> buildings = new List<BuildingBase>();
        [SerializeField] private List<Event> events = new List<Event>();
        [SerializeField] private List<TileList> tileLists = new List<TileList>();
        [SerializeField] private NavMeshSurface navMeshSurface;

        protected virtual void GeneratePlanet(TilesSO tilesSO, float radius)
        {
            for (int i = 0; i < tileLists.Count; i++)
            {
                if (i < tilesSO.tiles[i].Count)
                {
                    tileLists[i].SetTile(tilesSO.tiles[i].TileType, tilesSO.tiles[i].Color);
                }
                else
                {
                    tileLists[i].SetTile(TileType.Dead, Color.black);
                }
            }
            
            BakeNavMesh();
        }

        protected virtual void StartEvent()
        {
            foreach (var e in events)
            {
                if (Random.Range(0f, 1f) < e.Probability)
                {
                    e.StartEvent();
                    break;
                }
            }
        }
        
        protected virtual void BakeNavMesh()
        {
            navMeshSurface.BuildNavMesh();
            
        }
    }
}
