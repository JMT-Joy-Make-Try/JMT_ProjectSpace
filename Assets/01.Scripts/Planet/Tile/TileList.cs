using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileList : MonoBehaviour
    {
        [field: SerializeField] public List<PlanetTile> Tiles { get; private set; } = new List<PlanetTile>();
        private HashSet<Vector2> tilePositions = new HashSet<Vector2>();
        [SerializeField] private Fog _fog; 
        
        private LineRenderer lineRenderer;
        
        

        private void Awake()
        {
            Tiles = GetComponentsInChildren<PlanetTile>().ToList();
        }
        
        public void SetTile(TileType tileType, Color color)
        {
            foreach (var tile in Tiles)
            {
                tile.TileType = tileType;
                tile.Renderer.material.SetColor("_BaseColor", color);
            }
        }
        
        void UpdateTopBorder()
        {
        }
    }
}