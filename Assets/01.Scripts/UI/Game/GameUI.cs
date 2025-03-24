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
            interactionButton.onClick.AddListener(HandleInteractionButton);
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
            if (currentType != InteractType.Item)
            {
                TileManager.Instance.GetInteraction().Interaction();
                return;
            }
            AddEventTrigger(interactionButton, EventTriggerType.PointerDown, OnHoldStart);
            AddEventTrigger(interactionButton, EventTriggerType.PointerUp, OnHoldEnd);
        }

        private void OnHoldStart()
        {
            Debug.Log("시작");
            holdCoroutine = StartCoroutine(HoldCoroutine());
        }

        private void OnHoldEnd()
        {
            Debug.Log("떼다");
            StopCoroutine(holdCoroutine);
            RemoveEventTrigger(EventTriggerType.PointerDown);
            RemoveEventTrigger(EventTriggerType.PointerUp);
            isHold = false;
        }

        private void AddEventTrigger(Button button, EventTriggerType type, Action action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener((data) => action());
            interactTrigger.triggers.Add(entry);
        }
        private void RemoveEventTrigger(EventTriggerType type)
        {
            if(interactTrigger.triggers != null)
                interactTrigger.triggers.RemoveAll(entry => entry.eventID == type);
        }

        private IEnumerator HoldCoroutine(float time = 2f)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("다눌렀따");
            TileManager.Instance.GetInteraction().Interaction();
            isHold = true;
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
