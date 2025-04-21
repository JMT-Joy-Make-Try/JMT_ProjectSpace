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
                GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("건설할 수 없는 구역입니다.");
                return;
            }
            GameUIManager.Instance.ConstructCompo.OpenUI();
        }
    }
}
