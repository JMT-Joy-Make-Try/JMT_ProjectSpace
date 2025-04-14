using JMT.Building;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileList : MonoBehaviour
    {
        [field: SerializeField] public List<PlanetTile> Tiles { get; private set; } = new List<PlanetTile>();
        [SerializeField] private VillageBuilding _villageBuilding;
        [SerializeField] private Fog _fog; 
        [SerializeField] private Material _topBorderMaterial;
        private GameObject glowObject;
        public LineRenderer LineRenderer { get; private set; }

        private void Awake()
        {
            Tiles = GetComponentsInChildren<PlanetTile>().ToList();
            LineRenderer = GetComponent<LineRenderer>();
            LineRenderer.enabled = false;
        }

        public void SetTile(TileType tileType, Color color)
        {
            foreach (var tile in Tiles)
            {
                tile.TileType = tileType;
                tile.Renderer.material.SetColor("_BaseColor", color);
            }
            StartCoroutine(SpawnCo());
            
            
        }

        private IEnumerator SpawnCo()
        {
            bool isSpawned = false;
            PlanetTile tile = null;
            while (!isSpawned)
            {
                int idx = UnityEngine.Random.Range(0, Tiles.Count);
                PlanetTile tilePos = Tiles[idx];
                if (tilePos != null)
                {
                    if (tilePos.TryGetInteraction(out NoneInteraction interaction))
                    {
                        tile = tilePos; 
                        isSpawned = true;
                    }
                }
                
                yield return null;
            }
            Instantiate(_villageBuilding, tile.TileInteraction.transform);
            tile.RemoveInteraction();
            tile.AddInteraction<VillageInteraction>();
            yield return null;
        }
    }
}