using JMT.Building;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class BuildingInteraction : TileInteraction
    {
        public override void Interaction()
        {
            UIManager.Instance.BuildingUI.OpenUI();
        }
    }
}
