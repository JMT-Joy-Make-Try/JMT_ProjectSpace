using AYellowpaper.SerializedCollections;
using JMT.UISystem.Interact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileManager : MonoSingleton<TileManager>
    {
        [SerializeField] private PlanetTile _planetTile;
        public PlanetTile CurrentTile
        {
            get => _planetTile;
            set
            {
                if (_planetTile != null)
                    _planetTile.EdgeEnable(false);
                _planetTile = value;
                _planetTile.EdgeEnable(true);
            }
        }
        public Field.Field FieldPrefab;
        
        private Dictionary<Vector2Int, PlanetTile> _tileList = new();
        
        public PlanetTile GetTile(int x, int y)
        {
            Vector2Int position = new Vector2Int(x, y);
            if (_tileList.TryGetValue(position, out var tile))
            {
                return tile;
            }
            return null;
        }
        
        public void RegisterTile(int x, int y, PlanetTile tile)
        {
            Vector2Int position = new Vector2Int(x, y);
            if (!_tileList.ContainsKey(position))
            {
                _tileList[position] = tile;
            }
            else
            {
                Debug.LogWarning($"Tile at position {position} is already registered.");
            }
        }
        


        public TileInteraction GetInteraction() => CurrentTile.GetInteraction<TileInteraction>();
        public InteractType GetInteractType() => GetInteraction().InteractType;
    }
}
