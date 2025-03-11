using JMT.Building;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class BuildingInteraction : TileInteraction
    {
        public override void Interaction(PlanetTile tile)
        {
            UIManager.Instance.BuildingUI.OpenUI();
        }
    }
}
