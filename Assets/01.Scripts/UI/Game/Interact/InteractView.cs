using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JMT.UISystem.Interact
{
    public class InteractView : MonoBehaviour
    {
        public event Action OnInteractEvent;
        public event Action OnChangeInteractEvent;

        [SerializeField] private Sprite[] interactSprite;
        [SerializeField] private Button interactButton, changeButton;
        [SerializeField] private EventTrigger interactTrigger;
        [SerializeField] private Image interactionIcon;

        private void Awake()
        {
            interactButton.onClick.AddListener(() => OnInteractEvent?.Invoke());
            changeButton.onClick.AddListener(() => OnChangeInteractEvent?.Invoke());
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
    }
}
