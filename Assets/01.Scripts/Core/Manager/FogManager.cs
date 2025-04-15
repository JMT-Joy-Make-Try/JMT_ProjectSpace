using JMT.Planets.Tile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Manager
{
    public class FogManager : MonoSingleton<FogManager>
    {
        [SerializeField] private TileList _defaultTileList;
        [SerializeField] private List<TileList> _fogObjects1Tier;
        [SerializeField] private List<TileList> _fogObjects2Tier;
        [SerializeField] private List<TileList> _fogObjects3Tier;


        private void Start()
        {
            ResetFog();
            OffDefaultFog();
        }

        private void ResetFog()
        {
            _defaultTileList.Tiles.ForEach(t => t.Fog.SetFog(true));
            _fogObjects1Tier.ForEach(t => t.Tiles.ForEach(t => t.Fog.SetFog(true)));
            _fogObjects2Tier.ForEach(t => t.Tiles.ForEach(t => t.Fog.SetFog(true)));
            _fogObjects3Tier.ForEach(t => t.Tiles.ForEach(t => t.Fog.SetFog(true)));
        }

        public void OffFogBaseBuilding()
        {
            _fogObjects1Tier[0].Tiles.ForEach(t => t.Fog.SetFog(false));
        }
        
        public void OffDefaultFog()
        {
            _defaultTileList.Tiles.ForEach(t => t.Fog.SetFog(false));
        }
        
        
    }
}