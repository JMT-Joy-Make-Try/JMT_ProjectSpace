using JMT.Building;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class BuildingInteraction : TileInteraction
    {
        [SerializeField] private BuildingBase building;
        
        
        public override void Interaction(PlanetTile tile)
        {
            MainUI.Instance.BuildingUI.OpenUI();
            //tile.Build(building);
        }
    }
}
