using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class NoneInteraction : TileInteraction
    {
        public override void Interaction(PlanetTile tile)
        {
            base.Interaction(tile);
            UIManager.Instance.NoneUI.OpenUI();
        }
    }
}
