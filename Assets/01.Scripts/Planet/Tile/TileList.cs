using JMT.Building;
using System;
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
            
            int idx = UnityEngine.Random.Range(0, Tiles.Count);
            PlanetTile tilePos = Tiles[idx];
            var building = Instantiate(_villageBuilding, tilePos.TileInteraction.transform);
            tilePos.RemoveInteraction();
            tilePos.AddInteraction<VillageInteraction>();
        }
    }
}