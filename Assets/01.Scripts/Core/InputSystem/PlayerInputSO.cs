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
        public bool IsJoystickActive { get; private set; } = false;

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
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
            IsJoystickActive = context.phase != InputActionPhase.Canceled;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if(context.performed && !IsJoystickActive)
                OnLookEvent?.Invoke(context.ReadValue<Vector2>().x);
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
                case InputActionPhase.Performed:
                    OnLookEvent?.Invoke(context.ReadValue<float>());
                    break;
                case InputActionPhase.Canceled:
                    Debug.Log("뗌");
                    OnSecondaryEndEvent?.Invoke();
                    break;
            }
        }
    }
}
