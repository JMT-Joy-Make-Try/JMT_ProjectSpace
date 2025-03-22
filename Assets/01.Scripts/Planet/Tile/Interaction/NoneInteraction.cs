using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class NoneInteraction : TileInteraction
    {
        public override void Interaction()
        {
            UIManager.Instance.BuildPanelUI.OpenUI();
        }
    }
}
