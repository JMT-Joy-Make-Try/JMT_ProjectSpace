using JMT.Planets.Tile;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class ItemUI : PanelUI
    {
        private Button itemButton;

        private void Awake()
        {
            itemButton = PanelTrm.Find("ItemBtn").GetComponent<Button>();
            itemButton.onClick.AddListener(HandleMineButton);
        }

        private void HandleMineButton()
        {
            Debug.Log("Click Mine Button");
            TileManager.Instance._currentTile.RemoveInteractionObject();
            TileManager.Instance._currentTile.ChangeInteraction<NoneInteraction>();
            UIManager.Instance.ItemUI.CloseUI();
            // 자원 캐기
        }
    }
}
