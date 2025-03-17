using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JMT.Planets.Tile
{
    public class TileManager : MonoSingleton<TileManager>
    {
        private PlanetTile _planetTile;
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
    }
}
