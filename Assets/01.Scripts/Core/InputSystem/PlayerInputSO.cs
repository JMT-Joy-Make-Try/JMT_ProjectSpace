using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JMT
{
    [CreateAssetMenu(menuName = "SO/Input/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action<Vector2> OnMoveEvent;
        public event Action<float> OnLookEvent;
        public event Action OnSecondaryStartEvent;
        public event Action OnSecondaryEndEvent;

        private Controls controls;

        private Vector2 moveVec;

        private bool isMove = false;
        private bool isLook = false;

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
            if (context.started) isMove = true;
            else if (context.canceled) isMove = false;
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
            moveVec = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.started && !isMove)
            {
                isLook = true;
            }

            if (context.performed && isLook)
            {
                float delta = context.ReadValue<float>();
                if (!Mathf.Approximately(delta, 0f))
                {
                    OnLookEvent?.Invoke(delta);
                }
            }
            isLook = false;
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
                    if (isMove && !isLook) // 조이스틱 아닌 경우에만 회전 허용
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

    }
}
