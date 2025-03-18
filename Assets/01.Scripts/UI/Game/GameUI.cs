using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class GameUI : PanelUI
    {
        private Button inventoryButton, workButton;

        private void Awake()
        {
            inventoryButton = PanelTrm.Find("InvenBtn").GetComponent<Button>();
            workButton = PanelTrm.Find("WorkBtn").GetComponent<Button>();
            inventoryButton.onClick.AddListener(HandleInventoryButton);
            workButton.onClick.AddListener(HandleWorkButton);
        }

        private void HandleInventoryButton()
        {
            UIManager.Instance.InventoryUI.OpenUI();
        }

        private void HandleWorkButton()
        {
            UIManager.Instance.WorkUI.OpenUI();
        }
    }
}
