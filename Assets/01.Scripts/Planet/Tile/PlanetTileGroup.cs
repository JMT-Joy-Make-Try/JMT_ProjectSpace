using System.Collections.Generic;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class PlanetTileGroup : MonoBehaviour
    {
        public List<PlanetTile> Tiles { get; private set; } = new(4);
        private TileInteraction _tileInteraction;

        public T ChangeInteraction<T>() where T : TileInteraction
        {
            if (Tiles.Count == 0) return null;
            RemoveInteraction();
            AddInteraction<T>();
            return _tileInteraction as T;
        }
        
        public T AddInteraction<T>() where T : TileInteraction
        {
            if (Tiles.Count == 0) return null;

            _tileInteraction = Tiles[0].AddInteraction<T>();

            foreach (var tile in Tiles)
            {
                tile.TileInteraction = _tileInteraction;
            }
            return _tileInteraction as T;
        }
        
        public void RemoveInteraction()
        {
            if (_tileInteraction == null) return;

            foreach (var tile in Tiles)
            {
                tile.RemoveInteraction();
            }
            _tileInteraction = null;
        }
        
        public T GetInteraction<T>() where T : TileInteraction
        {
            if (_tileInteraction is T interaction)
            {
                return interaction;
            }
            return null;
        }
    }
}