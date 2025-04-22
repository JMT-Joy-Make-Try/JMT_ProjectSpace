using JMT.Planets.Tile;
using JMT.UISystem.Interact;
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
        private Button inventoryButton, interactButton, changeBtn;
        private EventTrigger interactTrigger;
        private Image interactionIcon;
        private Coroutine holdCoroutine;

        private bool isHold;

        private void Awake()
        {
            inventoryButton = PanelTrm.Find("InvenBtn").GetComponent<Button>();
            interactButton = PanelTrm.Find("InteractBtn").GetComponent<Button>();
            changeBtn = PanelTrm.Find("ChangeBtn").GetComponent<Button>();
            interactionIcon = interactButton.transform.Find("Icon").GetComponent<Image>();
            interactTrigger = interactButton.GetComponent<EventTrigger>();

            inventoryButton.onClick.AddListener(HandleInventoryButton);
            changeBtn.onClick.AddListener(HandleAttackButton);
            AddEventTrigger(EventTriggerType.PointerDown, HandleInteractionButton);

            GameUIManager.Instance.InteractCompo.OnChangeInteractEvent += HandleChangeInteract;
        }

        private void HandleChangeInteract()
        {
            var type = GameUIManager.Instance.InteractCompo.InteractType;
            interactionIcon.sprite = interactSprite[(int)type];
        }

        private void HandleInteractionButton()
        {
            InteractType type = GameUIManager.Instance.InteractCompo.InteractType;

            Debug.Log("type : " + type);
            if (type == InteractType.Attack)
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
            if (GameUIManager.Instance.InteractCompo.InteractType != InteractType.Attack)
                type = InteractType.Attack;

            GameUIManager.Instance.InteractCompo.ChangeInteract(type);
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
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(true, "재료 캐는 중...");
            holdCoroutine = StartCoroutine(HoldCoroutine());
        }

        private void OnHoldEnd()
        {
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(false);
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
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(false);
            OnHoldEvent?.Invoke(false);
            isHold = true;
        }

        private void HandleInventoryButton()
            => GameUIManager.Instance.InventoryCompo.OpenUI();
    }
}
