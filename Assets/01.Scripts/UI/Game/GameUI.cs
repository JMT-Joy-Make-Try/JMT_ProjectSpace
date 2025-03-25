using JMT.Planets.Tile;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class GameUI : PanelUI
    {
        [SerializeField] private Sprite build, mine, building;
        private Button inventoryButton, workButton, interactionButton;
        private EventTrigger interactTrigger;
        private Image interactionIcon;
        private InteractType currentType;
        private Coroutine holdCoroutine;

        private bool isHold;

        private void Awake()
        {
            inventoryButton = PanelTrm.Find("InvenBtn").GetComponent<Button>();
            workButton = PanelTrm.Find("WorkBtn").GetComponent<Button>();
            interactionButton = PanelTrm.Find("InteractionBtn").GetComponent<Button>();
            interactionIcon = interactionButton.transform.Find("Icon").GetComponent<Image>();
            interactTrigger = interactionButton.GetComponent<EventTrigger>();

            inventoryButton.onClick.AddListener(HandleInventoryButton);
            workButton.onClick.AddListener(HandleWorkButton);
            AddEventTrigger(EventTriggerType.PointerDown, HandleInteractionButton);
        }

        public void ChangeInteract(InteractType type)
        {
            currentType = type;
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
            if (currentType != InteractType.Item && !isHold)
            {
                TileManager.Instance.GetInteraction().Interaction();
                return;
            }
            AddEventTrigger(EventTriggerType.PointerDown, OnHoldStart);
            AddEventTrigger(EventTriggerType.PointerUp, OnHoldEnd);
        }

        private void AddEventTrigger(EventTriggerType type, Action action)
        {
            var entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener((data) => action());
            interactTrigger.triggers.Add(entry);
        }
        private void RemoveEventTrigger(EventTriggerType type)
        {
            interactTrigger.triggers.RemoveAll(entry => entry.eventID == type);
        }

        private void OnHoldStart()
        {
            UIManager.Instance.PopupUI.SetInteractPopup("재료 캐는 중...");
            UIManager.Instance.PopupUI.ActiveInteractPopup(true);
            holdCoroutine = StartCoroutine(HoldCoroutine());
        }

        private void OnHoldEnd()
        {
            UIManager.Instance.PopupUI.ActiveInteractPopup(false);
            if (holdCoroutine != null)
            {
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;
            }

            RemoveEventTrigger(EventTriggerType.PointerDown);
            RemoveEventTrigger(EventTriggerType.PointerUp);
            isHold = false;
            AddEventTrigger(EventTriggerType.PointerDown, HandleInteractionButton);
        }

        private IEnumerator HoldCoroutine(float time = 1f)
        {
            yield return new WaitForSeconds(time);
            TileManager.Instance.GetInteraction().Interaction();
            UIManager.Instance.PopupUI.ActiveInteractPopup(false);
            isHold = true;
        }

        private void HandleInventoryButton()
            => UIManager.Instance.InventoryUI.OpenUI();

        private void HandleWorkButton()
            => UIManager.Instance.WorkUI.OpenUI();
    }
}
