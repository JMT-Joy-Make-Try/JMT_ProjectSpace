using JMT.Agent;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.UISystem.Interact
{
    public class InteractController : MonoBehaviour
    {
        public event Action<bool> OnHoldEvent;
        public event Action OnAnimationEndEvent;
        public event Action OnClickEvent;

        [SerializeField] private InteractView view;
        private InteractModel model = new();
        private Coroutine holdCoroutine;
        private bool isHold = false;
        private bool _isHoldEnd = false;

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
            OnHoldEvent?.Invoke(true);
        }
        
        public void StopInfinityHold()
        {
            OnHoldEvent?.Invoke(false);
            isHold = false;
            EndHold();
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
            else if (type.Equals(InteractType.Holding))
                view.SetHoldEventTrigger(InfinityHold, StopInfinityHold);
            else if (type.Equals(InteractType.FieldHold))
                view.SetHoldEventTrigger(OnFieldHoldStart, OnFieldHoldEnd);
            else
            {
                view.AddEventTrigger(EventTriggerType.PointerDown, HandleInteraction);
            }

        }
        
        

        private void HandleInteraction()
        {
            InteractType type = model.InteractType;

            if (!type.Equals(InteractType.Item))
            {
                TileManager.Instance.GetInteraction()?.Interaction();
                OnClickEvent?.Invoke();
            }
        }


        private void OnHoldStart()
        {
            GameUIManager.Instance.PlayerControlActive(false);
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(true, "재료 캐는 중...");
            var currentInteract = TileManager.Instance.GetInteraction();
            var interactTime = AgentManager.Instance.Player.StatCompo.GetInteractTime(currentInteract.GetItemType());
            holdCoroutine = StartCoroutine(HoldCoroutine(interactTime));
            AgentManager.Instance.Player.AnimatorCompo.SetLayer(0, 1);
        }
        
        public void OnFieldHoldStart()
        {
            GameUIManager.Instance.PlayerControlActive(false);
            GameUIManager.Instance.PopupCompo.SetActiveFixPopup(true, "밭 가는 중...");
            AgentManager.Instance.Player.AnimatorCompo.SetLayer(3, 1);
            holdCoroutine = StartCoroutine(HoldCoroutine(12));
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
        }
    }
}
