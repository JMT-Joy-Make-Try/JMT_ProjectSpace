using JMT.Core.Tool;
using JMT.Planets.Tile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Manager
{
    public class FogManager : MonoSingleton<FogManager>
    {
        [SerializeField] private bool _isFogActive = true;
        [Space]
        [SerializeField] private TileList _defaultTileList;
        [SerializeField] private TileList _baseBuildingTileList;
        [SerializeField] private List<TileList> _fogObjects1Tier;
        [SerializeField] private List<TileList> _fogObjects2Tier;
        [SerializeField] private List<TileList> _fogObjects3Tier;

        private void Start()
        {
            ResetFog(_isFogActive);
            OffDefaultFog();
        }

        private void ResetFog(bool active)
        {
            _defaultTileList.Tiles.ForEach(t => t.Fog.SetFog(active));
            _fogObjects1Tier.ForEach(t => t.Tiles.ForEach(tile => tile.Fog.SetFog(active)));
            _fogObjects2Tier.ForEach(t => t.Tiles.ForEach(tile => tile.Fog.SetFog(active)));
            _fogObjects3Tier.ForEach(t => t.Tiles.ForEach(tile => tile.Fog.SetFog(active)));
        }

        public void OffFogBaseBuilding()
        {
            _baseBuildingTileList.Tiles.ForEach(t => t.Fog.SetFog(false));
        }

        public void OffDefaultFog()
        {
            _defaultTileList.Tiles.ForEach(t => t.Fog.SetFog(false));
        }

        public bool IsAllFogOff(FogTier tier)
        {
            if (tier == FogTier.None) return true;

            var targetList = tier switch
            {
                FogTier.One => _fogObjects1Tier,
                FogTier.Two => _fogObjects2Tier,
                FogTier.Three => _fogObjects3Tier,
                _ => throw new ArgumentOutOfRangeException(nameof(tier), tier, null)
            };

            foreach (var tileList in targetList)
            {
                foreach (var tile in tileList.Tiles)
                {
                    if (tile.Fog.IsFogActive)
                        return false;
                }
            }

            return true;
        }
    }
}
