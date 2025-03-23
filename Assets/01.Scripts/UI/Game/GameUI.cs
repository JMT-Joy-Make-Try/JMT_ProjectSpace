using JMT.Planets.Tile;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class GameUI : PanelUI
    {
        [SerializeField] private Sprite build, mine, building;
        private Button inventoryButton, workButton, interactionButton;

        private void Awake()
        {
            inventoryButton = PanelTrm.Find("InvenBtn").GetComponent<Button>();
            workButton = PanelTrm.Find("WorkBtn").GetComponent<Button>();
            interactionButton = PanelTrm.Find("InteractionBtn").GetComponent<Button>();
            inventoryButton.onClick.AddListener(HandleInventoryButton);
            workButton.onClick.AddListener(HandleWorkButton);
            interactionButton.onClick.AddListener(HandleInteractionButton);
        }

        private void HandleInteractionButton()
        {
            TileManager.Instance.GetInteraction().Interaction();
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
