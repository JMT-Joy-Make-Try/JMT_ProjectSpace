using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JMT.UISystem.Interact
{
    public class InteractView : MonoBehaviour
    {
        public event Action OnInteractEvent;
        public event Action OnChangeInteractEvent;

        [SerializeField] private Sprite[] interactSprite;
        [SerializeField] private Button interactButton;
        [SerializeField] private Button extraInteractButton;
        [SerializeField] private Image interactLine;
        [SerializeField] private EventTrigger interactTrigger;
        [SerializeField] private Image interactionIcon;

        private Tween lineTween;

        private void Awake()
        {
            interactButton.onClick.AddListener(HandleInteractButton);
        }

        private void OnDestroy()
        {
            interactButton.onClick.RemoveListener(HandleInteractButton);
            extraInteractButton.onClick.RemoveAllListeners();
        }

        private void HandleInteractButton()
        {
            OnInteractEvent?.Invoke();
        }

        public void SetExtraButton(bool isTrue, UnityAction action = null)
        {
            extraInteractButton.onClick.RemoveAllListeners();
            extraInteractButton.gameObject.SetActive(isTrue);
            if (action != null)
            extraInteractButton.onClick.AddListener(action);
        }

        public void ChangeInteract(InteractType type)
        {
            interactionIcon.sprite = interactSprite[(int)type];
        }

        public void SetHoldEventTrigger(Action onDown, Action onUp)
        {
            RemoveAllEventTriggers();
            AddEventTrigger(EventTriggerType.PointerDown, onDown);
            AddEventTrigger(EventTriggerType.PointerUp, onUp);
        }

        public void AddEventTrigger(EventTriggerType type, Action action)
        {
            var entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener((data) => action());
            interactTrigger.triggers.Add(entry);
        }

        public void RemoveAllEventTriggers() => interactTrigger?.triggers.Clear();

        private void RemoveEventTrigger(EventTriggerType type)
        {
            interactTrigger.triggers.RemoveAll(entry => entry.eventID == type);
        }

        public void StartInteractLine()
        {
            lineTween = interactLine.rectTransform.DOLocalRotate(new(0, 0, 360), 10f, RotateMode.FastBeyond360).SetLoops(-1);
        }

        public void StopInteractLine()
        {
            lineTween?.Kill();
        }
    }
}
