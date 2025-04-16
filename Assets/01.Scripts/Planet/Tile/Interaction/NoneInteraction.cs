using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class NoneInteraction : TileInteraction
    {
        public override void Interaction()
        {
            if (TileManager.Instance.CurrentTile.Fog.IsFogActive)
            {
                UIManager.Instance.PopupUI.SetPopupText("건설할 수 없는 구역입니다.");
                return;
            }
            UIManager.Instance.BuildPanelUI.OpenUI();
        }
    }
}
