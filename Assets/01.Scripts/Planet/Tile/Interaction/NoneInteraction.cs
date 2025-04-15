using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class NoneInteraction : TileInteraction
    {
        public override void Interaction()
        {
            if (TileManager.Instance.CurrentTile.Fog.IsFogActive) return;
            UIManager.Instance.BuildPanelUI.OpenUI();
        }
    }
}
