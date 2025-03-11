using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class GameUI : PanelUI
    {
        private Button inventoryButton;

        private void Awake()
        {
            inventoryButton = PanelTrm.Find("InvenBtn").GetComponent<Button>();
            inventoryButton.onClick.AddListener(HandleInventoryButton);
        }

        private void HandleInventoryButton()
        {
            UIManager.Instance.InventoryUI.OpenUI();
        }
    }
}
