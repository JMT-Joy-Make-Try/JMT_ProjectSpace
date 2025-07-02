using JMT.Agent;
using JMT.Planet.Tile;
using JMT.PlayerCharacter;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class NoneInteraction : TileInteraction
    {
        
        public override void Interaction()
        {
            if (!IsPlayerHaveTool())
            {
                // if (TileManager.Instance.CurrentTile.Fog.IsFogActive)
                // {
                //     GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("건설할 수 없는 구역입니다.");
                //     return;
                // }

                GameUIManager.Instance.ConstructCompo.OpenUI();
            }
            else
            {
                planetTile.ChangeInteraction<FieldHoldInteraction>();
            }
        }

        private bool IsPlayerHaveTool()
        {
            Player player = AgentManager.Instance.Player;
            if (player == null) return false;
            
            var playerToolCompo = player.PlayerToolCompo;
            
            if (playerToolCompo.CurPlayerToolSO?.ToolType == PlayerToolType.Farmer)
                return true;
            
            return false;
        }
    }
}
