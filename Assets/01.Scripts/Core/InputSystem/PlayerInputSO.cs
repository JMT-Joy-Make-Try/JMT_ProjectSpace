using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JMT
{
    public enum TouchType
    {
        None,
        FirstTouch,
        SecondTouch,
    }
    [CreateAssetMenu(menuName = "SO/Input/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action<Vector2> OnMoveEvent;
        public event Action<float> OnLookEvent;
        public event Action OnSecondaryStartEvent;
        public event Action OnSecondaryEndEvent;

        private Controls controls;
        private TouchType touchType = TouchType.None;

        private Vector2 moveVec;

        private bool isMove = false;
        private bool isFirst = false;
        private bool isSecond = false;

        private void OnEnable() => ControlEnable();
        private void OnDisable() => ControlDisable();

        public void ControlEnable()
        {
            if (controls == null)
                controls = new Controls();

            controls.Player.AddCallbacks(this);
            controls.Enable();
        }

        public void ControlDisable() => controls.Disable();

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if(isFirst) touchType = TouchType.SecondTouch;
                else touchType = TouchType.FirstTouch;
                isMove = true;
            }
            else if (context.canceled) isMove = false;
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
            moveVec = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (touchType == TouchType.FirstTouch) return;

            Vector2 delta = context.ReadValue<Vector2>();
            if (!Mathf.Approximately(delta.x, 0f))
            {
                OnLookEvent?.Invoke(delta.x);
            }
        }

        public void OnAttack(InputAction.CallbackContext context) { }
        public void OnInteract(InputAction.CallbackContext context) { }
        public void OnCrouch(InputAction.CallbackContext context) { }
        public void OnJump(InputAction.CallbackContext context) { }
        public void OnPrevious(InputAction.CallbackContext context) { }
        public void OnNext(InputAction.CallbackContext context) { }
        public void OnSprint(InputAction.CallbackContext context) { }

        public void OnSecondary(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    OnSecondaryStartEvent?.Invoke();
                    break;

                case InputActionPhase.Performed:
                    if (touchType == TouchType.FirstTouch)
                    {
                        Vector2 delta = context.ReadValue<Vector2>();
                        if (!Mathf.Approximately(delta.x, 0f))
                            OnLookEvent?.Invoke(delta.x);
                    }
                    break;

                case InputActionPhase.Canceled:
                    OnSecondaryEndEvent?.Invoke();
                    break;
            }
        }

        public void OnFirstContact(InputAction.CallbackContext context)
        {
            if (context.started)
                isFirst = true;
            else if (context.canceled)
            {
                if (touchType == TouchType.FirstTouch)
                    touchType = TouchType.None;
                isFirst = false;
            }
        }

        public void OnSecondContact(InputAction.CallbackContext context)
        {
            if (context.started)
                isSecond = true;
            else if (context.canceled)
            {
                if (touchType == TouchType.SecondTouch)
                    touchType = TouchType.None;
                isSecond = false;
            }
        }
    }
}
