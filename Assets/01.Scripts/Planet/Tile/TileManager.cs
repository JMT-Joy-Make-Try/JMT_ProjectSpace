using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JMT.Planets.Tile
{
    public enum InteractType
    {
        None,
        Item,
        Building,
    }
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

        public InteractType GetInteractType()
        {
            switch(GetInteraction())
            {
                case NoneInteraction:
                    return InteractType.None;
                case ItemInteraction:
                    return InteractType.Item;
                case BuildingInteraction:
                    return InteractType.Building;
            }
            return InteractType.None;
        }
    }
}
