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
        public event Action<bool> OnHoldEvent;
        public event Action OnAttackEvent;

        [SerializeField] private Sprite[] interactSprite;
        private Button inventoryButton, workButton, interactButton, changeBtn;
        private EventTrigger interactTrigger;
        private Image interactionIcon;
        private Coroutine holdCoroutine;

        private bool isHold;

        private void Awake()
        {
            inventoryButton = PanelTrm.Find("InvenBtn").GetComponent<Button>();
            workButton = PanelTrm.Find("WorkBtn").GetComponent<Button>();
            interactButton = PanelTrm.Find("InteractBtn").GetComponent<Button>();
            changeBtn = PanelTrm.Find("ChangeBtn").GetComponent<Button>();
            interactionIcon = interactButton.transform.Find("Icon").GetComponent<Image>();
            interactTrigger = interactButton.GetComponent<EventTrigger>();

            inventoryButton.onClick.AddListener(HandleInventoryButton);
            workButton.onClick.AddListener(HandleWorkButton);
            changeBtn.onClick.AddListener(HandleAttackButton);
            AddEventTrigger(EventTriggerType.PointerDown, HandleInteractionButton);

            InteractSystem.Instance.OnChangeInteractEvent += HandleChangeInteract;
        }

        private void HandleChangeInteract(InteractType type)
        {
            InteractSystem.Instance.ChangeInteract(type);
            interactionIcon.sprite = interactSprite[(int)type];
        }

        private void HandleInteractionButton()
        {
            InteractType type = InteractSystem.Instance.InteractType;

            Debug.Log("type : " + type);
            if(type == InteractType.Attack)
            {
                OnAttackEvent?.Invoke();
            }
            else if (type != InteractType.Item && !isHold)
            {
                TileManager.Instance.GetInteraction().Interaction();
                return;
            }
            else
            {
                AddEventTrigger(EventTriggerType.PointerDown, OnHoldStart);
                AddEventTrigger(EventTriggerType.PointerUp, OnHoldEnd);
            }
        }

        private void HandleAttackButton()
        {
            InteractType type = InteractType.None;
            if (InteractSystem.Instance.InteractType != InteractType.Attack)
                type = InteractType.Attack;
            
            InteractSystem.Instance.ChangeInteract(type);
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
                OnHoldEvent?.Invoke(false);
            }

            RemoveEventTrigger(EventTriggerType.PointerDown);
            RemoveEventTrigger(EventTriggerType.PointerUp);
            isHold = false;
            AddEventTrigger(EventTriggerType.PointerDown, HandleInteractionButton);
        }

        private IEnumerator HoldCoroutine(float time = 1f)
        {
            OnHoldEvent?.Invoke(true);
            yield return new WaitForSeconds(time);
            TileManager.Instance.GetInteraction().Interaction();
            UIManager.Instance.PopupUI.ActiveInteractPopup(false);
            OnHoldEvent?.Invoke(false);
            isHold = true;
        }

        private void HandleInventoryButton()
            => UIManager.Instance.InventoryUI.OpenUI();

        private void HandleWorkButton()
            => UIManager.Instance.WorkUI.OpenUI();
    }
}
