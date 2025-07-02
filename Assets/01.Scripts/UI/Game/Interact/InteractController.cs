using JMT.Agent;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using JMT.PlayerCharacter;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace JMT.UISystem.Interact
{
    public class InteractController : MonoBehaviour
    {
        public event Action<bool> OnHoldEvent;
        public event Action OnHoldCancelEvent;
        public event Action OnAnimationEndEvent;
        public event Action OnClickEvent;
        public event Action OnTraderInteractEvent;

        [SerializeField] private InteractView view;
        private InteractModel model = new();
        private Coroutine holdCoroutine;
        private bool isHold = false;
        private bool _isHoldEnd = false;

        private UnityAction action =
            () => AgentManager.Instance.Player.AnimatorCompo.ChangeState(PlayerState.ReturnBase);

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

        private void OnDestroy()
        {
            view.OnChangeInteractEvent -= HandleChangeInteract;
        }
        
        public void InfinityHold()
        {
            GameUIManager.Instance.PlayerControlActive(false);
            OnHoldEvent?.Invoke(true);
            view.StartInteractLine();
        }
        
        public void StopInfinityHold()
        {
            GameUIManager.Instance.PlayerControlActive(true);
            OnHoldEvent?.Invoke(false);
            isHold = false;
            EndHold();
        }
        
        public void SetExtraButton(bool isTrue, UnityAction action)
            => view.SetExtraButton(isTrue, action);

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
            view.SetExtraButton(false);
            switch (type)
            {
                case InteractType.Item:
                    view.SetHoldEventTrigger(OnHoldStart, OnHoldEnd);
                    break;
                case InteractType.Holding:
                    view.SetHoldEventTrigger(InfinityHold, StopInfinityHold);
                    break;
                case InteractType.FieldHold:
                    view.SetHoldEventTrigger(OnFieldHoldStart, OnFieldHoldEnd);
                    break;
                case InteractType.Trader:
                    view.AddEventTrigger(EventTriggerType.PointerDown, HandleTraderInteraction);
                    break;
                case InteractType.Station:
                    view.SetExtraButton(true, action);
                    view.AddEventTrigger(EventTriggerType.PointerDown, HandleInteraction);
                    break;
                default:
                    view.AddEventTrigger(EventTriggerType.PointerDown, HandleInteraction);
                    break;
            }

        }

        private void HandleTraderInteraction()
        {
            OnTraderInteractEvent?.Invoke();
        }


        private void HandleInteraction()
        {
            InteractType type = model.InteractType;

            if (type.Equals(InteractType.Station))
            {
                if (!AgentManager.Instance.Player.InventoryCompo.IsPlayerHoldingItem())
                {
                    TileManager.Instance.GetInteraction()?.Interaction();
                }
                OnClickEvent?.Invoke();
            }
            else if (!type.Equals(InteractType.Item))
            {
                OnClickEvent?.Invoke();
                TileManager.Instance.GetInteraction()?.Interaction();
            }
            
        }


        private void OnHoldStart()
        {
            GameUIManager.Instance.PlayerControlActive(false);
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(true, "재료 캐는 중...");
            var currentInteract = TileManager.Instance.GetInteraction();
            var interactTime = AgentManager.Instance.Player.StatCompo.GetInteractTime(currentInteract.GetItemType());
            holdCoroutine = StartCoroutine(HoldCoroutine(interactTime));
            AgentManager.Instance.Player.AnimatorCompo.SetLayer(PlayerCharacter.PlayerAnimationLayer.BaseLayer, 1);
            view.StartInteractLine();
        }
        
        public void OnFieldHoldStart()
        {
            GameUIManager.Instance.PlayerControlActive(false);
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(true, "밭 가는 중...");
            AgentManager.Instance.Player.AnimatorCompo.SetLayer(PlayerCharacter.PlayerAnimationLayer.FieldLayer, 1);
            holdCoroutine = StartCoroutine(HoldCoroutine(5));
            view.StartInteractLine();
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

        private void OnFieldHoldEnd()
        {
            if (holdCoroutine != null)
            {
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;
                if (_isHoldEnd)
                    OnHoldEvent?.Invoke(false);
                else
                    OnHoldCancelEvent?.Invoke();
                
                OnAnimationEndEvent?.Invoke();
            }
            isHold = false;
            _isHoldEnd = false;
            EndHold();
        }

        private IEnumerator HoldCoroutine(float time = 1f)
        {
            _isHoldEnd = false;
            OnHoldEvent?.Invoke(true);
            yield return new WaitForSeconds(time);
            TileManager.Instance.GetInteraction().Interaction();
            isHold = true;
            _isHoldEnd = true;

            OnHoldEnd();
        }

        private void EndHold()
        {
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(false);
            GameUIManager.Instance.PlayerControlActive(true);
            view.StopInteractLine();
        }
    }
}
