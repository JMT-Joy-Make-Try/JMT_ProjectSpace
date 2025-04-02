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

        private void Awake()
        {
            Tiles = GetComponentsInChildren<PlanetTile>().ToList();
        }

        private void Start()
        {
            foreach (var VARIABLE in Tiles)
            {
                
            }
        }

        private void OnPostRender()
        {
            
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