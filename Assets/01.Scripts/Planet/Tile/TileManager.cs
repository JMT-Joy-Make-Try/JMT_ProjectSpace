using System;
using UnityEngine;
using UnityEngine.Serialization;

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
        public TileInteraction GetInteraction() => CurrentTile.transform.GetComponentInChildren<TileInteraction>();

        public InteractType GetInteractType() => GetInteraction().InteractType;
    }
}
