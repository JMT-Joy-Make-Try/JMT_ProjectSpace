using System.Collections.Generic;
using JMT.Building;
using JMT.Planets.Tile;
using UnityEngine;
using Event = JMT.Planets.Events.Event;

namespace JMT.Planets
{
    public abstract class Planet : MonoBehaviour
    {
        [SerializeField] private List<BuildingBase> buildings = new List<BuildingBase>();
        [SerializeField] private List<Event> events = new List<Event>();
        [SerializeField] private List<TileList> tileLists = new List<TileList>();

        protected virtual void GeneratePlanet(TilesSO tilesSO, float radius)
        {
            foreach (var tileData in tilesSO.tiles)
            {
                foreach (var tileList in tileLists)
                {
                    int cnt = 0;
                    foreach (var tile in tileList.Tiles)
                    {
                        if (cnt >= tileData.Count)
                        {
                            tile.TileType = TileType.Dead;
                            tile.Renderer.material.color = Color.black;
                        }
                        else
                        {
                            tile.TileType = tileData.TileType;
                            tile.Renderer.material.color = tileData.Color;
                        }

                        cnt++;
                    }
                }
            }
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
    }
}
