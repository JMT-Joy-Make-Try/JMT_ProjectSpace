using System;
using System.Collections.Generic;
using UnityEngine;
namespace JMT.Planets.Tile
{
    public class PlanetTileGroup : MonoBehaviour
    {
        public List<PlanetTile> Tiles => _tiles;
        public TileInteraction Interaction => _interaction;

        [SerializeField] private List<PlanetTile> _tiles = new List<PlanetTile>();
        private TileInteraction _interaction;

        public void SetInteraction(TileInteraction interaction)
        {
            _interaction = interaction;
        }

        public T GetInteraction<T>() where T : TileInteraction
        {
            if (_interaction == null)
            {
                Debug.LogError("Interaction is not set for this tile group.");
                return null;
            }
            return _interaction as T;
        }

        public void ChangeInteraction<T>() where T : TileInteraction
        {
            foreach (var tile in _tiles)
            {
                if (tile.TileInteraction != null)
                {
                    tile.RemoveInteraction();
                }
                tile.AddInteraction<T>();
            }
        }
    }
}