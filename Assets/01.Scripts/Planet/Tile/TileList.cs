using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileList : MonoBehaviour
    {
        [field: SerializeField] public List<PlanetTile> Tiles { get; private set; } = new List<PlanetTile>();
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
        }
    }
}