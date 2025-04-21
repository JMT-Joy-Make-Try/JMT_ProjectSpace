using JMT.Planets.Tile;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.UISystem.Interact
{
    public class InteractController : MonoBehaviour
    {
        public event Action<bool> OnHoldEvent;
        public event Action OnAttackEvent;

        [SerializeField] private InteractView view;
        private InteractModel model = new();
        private Coroutine holdCoroutine;
        private bool isHold = false;

        public InteractType InteractType => model.InteractType;
        public event Action OnChangeInteractEvent
        {
            add => view.OnChangeInteractEvent += value;
            remove => view.OnChangeInteractEvent -= value;
        }

        private void Awake()
        {
            view.OnInteractEvent += HandleInteraction;
            view.OnChangeInteractEvent += HandleChangeInteract;
        }

        private void HandleChangeInteract()
        {
            InteractType type = InteractType.None;
            if (model.InteractType != InteractType.Attack)
                type = InteractType.Attack;

            ChangeInteract(type);
        }

        public void ChangeInteract(InteractType type)
        {
            model.ChangeInteract(type);
            view.ChangeInteract(type);
        }

        private void HandleInteraction()
        {
            InteractType type = model.InteractType;

            Debug.Log("type : " + type);
            if (type == InteractType.Attack)
                OnAttackEvent?.Invoke();

            else if (type != InteractType.Item && !isHold)
                TileManager.Instance.GetInteraction().Interaction();

            else
                view.SetHoldEventTrigger(OnHoldStart, OnHoldEnd);
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

            view.RemoveAllEventTriggers();
            isHold = false;
            view.AddEventTrigger(EventTriggerType.PointerDown, HandleInteraction);
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
    }
}
