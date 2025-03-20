using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JMT
{
    [CreateAssetMenu(menuName = "SO/Input/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action<Vector2> OnMoveEvent;
        public event Action<Vector2> OnLookEvent;
        public event Action OnRotateStartEvent;
        public event Action OnRotateEndEvent;
        public event Action OnSecondaryStartEvent;
        public event Action OnSecondaryEndEvent;

        private Controls controls;
        public bool IsJoystickActive { get; private set; } = false;

        private void OnEnable()
        {
            if (controls == null)
            {
                controls = new Controls();
                controls.Player.AddCallbacks(this);
                controls.Enable();
            }
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
            IsJoystickActive = context.phase != InputActionPhase.Canceled;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                OnRotateStartEvent?.Invoke();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                OnRotateEndEvent?.Invoke();
            }
            else
            {
                OnLookEvent?.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
        }

        public void OnJump(InputAction.CallbackContext context)
        {
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
        }

        public void OnNext(InputAction.CallbackContext context)
        {
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
        }

        public void OnSecondary(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Debug.Log("누름");
                    OnSecondaryStartEvent?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    Debug.Log("뗌");
                    OnSecondaryEndEvent?.Invoke();
                    break;
            }
        }
    }
}
