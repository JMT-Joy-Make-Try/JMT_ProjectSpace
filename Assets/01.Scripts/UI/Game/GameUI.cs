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
        private Image interactionIcon;

        private void Awake()
        {
            inventoryButton = PanelTrm.Find("InvenBtn").GetComponent<Button>();
            workButton = PanelTrm.Find("WorkBtn").GetComponent<Button>();
            interactionButton = PanelTrm.Find("InteractionBtn").GetComponent<Button>();
            interactionIcon = interactionButton.transform.Find("Icon").GetComponent<Image>();
            inventoryButton.onClick.AddListener(HandleInventoryButton);
            workButton.onClick.AddListener(HandleWorkButton);
            interactionButton.onClick.AddListener(HandleInteractionButton);
        }

        public void ChangeInteract(InteractType type)
        {
            switch (type)
            {
                case InteractType.None:
                    interactionIcon.sprite = build;
                    break;
                case InteractType.Item:
                    interactionIcon.sprite = mine;
                    break;
                case InteractType.Building:
                    interactionIcon.sprite = building;
                    break;
            }
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
