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
            //view.OnInteractEvent += HandleInteraction;
            view.OnChangeInteractEvent += HandleChangeInteract;
        }

        private void HandleChangeInteract()
        {
            InteractType type = InteractType.None;
            if (!model.InteractType.Equals(InteractType.Attack))
                type = InteractType.Attack;

            ChangeInteract(type);
        }

        public void ChangeInteract(InteractType type)
        {
            model.ChangeInteract(type);
            view.ChangeInteract(type);

            view.RemoveAllEventTriggers();
            if (type.Equals(InteractType.Item))
                view.SetHoldEventTrigger(OnHoldStart, OnHoldEnd);
            else
            {
                view.AddEventTrigger(EventTriggerType.PointerDown, HandleInteraction);
            }

        }

        private void HandleInteraction()
        {
            InteractType type = model.InteractType;

            Debug.Log("type : " + type);
            if (type.Equals(InteractType.Attack))
                OnAttackEvent?.Invoke();

            else if (!type.Equals(InteractType.Item))
                TileManager.Instance.GetInteraction().Interaction();
        }


        private void OnHoldStart()
        {
            GameUIManager.Instance.PlayerControlActive(false);
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(true, "재료 캐는 중...");
            holdCoroutine = StartCoroutine(HoldCoroutine());
        }

        private void OnHoldEnd()
        {
            if (holdCoroutine != null)
            {
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;
                OnHoldEvent?.Invoke(false);
            }
            isHold = false;
            EndHold();
        }

        private IEnumerator HoldCoroutine(float time = 1f)
        {
            OnHoldEvent?.Invoke(true);
            yield return new WaitForSeconds(time);
            TileManager.Instance.GetInteraction().Interaction();
            isHold = true;

            EndHold();
        }

        private void EndHold()
        {
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(false);
            GameUIManager.Instance.PlayerControlActive(true);
        }
    }
}
