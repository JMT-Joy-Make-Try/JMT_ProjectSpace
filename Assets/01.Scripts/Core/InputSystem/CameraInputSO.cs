using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JMT.InputSystem
{
    [CreateAssetMenu(menuName = "SO/Input/InputSO")]
    public class CameraInputSO : ScriptableObject, Controls.IScreenTouchActions
    {
        public event Action OnRotateStartEvent;
        public event Action OnRotateEndEvent;
        public event Action OnZoomStartEvent;
        public event Action OnZoomEndEvent;

        private Controls controls;

        private void OnEnable()
        {
            if (controls == null)
            {
                controls = new Controls();
                controls.ScreenTouch.SetCallbacks(this);
                controls.ScreenTouch.Enable();
            }
        }

        private void OnDisable()
        {
            controls.ScreenTouch.Disable();
        }

        public Vector2 GetPrimaryPosition() => controls.ScreenTouch.PrimaryTouch.ReadValue<Vector2>();
        public Vector2 GetSecondaryPosition() => controls.ScreenTouch.SecondaryTouch.ReadValue<Vector2>();

        public void OnSecondaryTouchContact(InputAction.CallbackContext context)
        {
            switch(context.phase)
            {
                case InputActionPhase.Started:
                    OnRotateEndEvent?.Invoke();
                    OnZoomStartEvent?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    OnZoomEndEvent?.Invoke();
                    break;
            }
        }

        public void OnPrimaryTouchContact(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    OnRotateStartEvent?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    OnRotateEndEvent?.Invoke();
                    break;
            }
        }

        public void OnPrimaryTouch(InputAction.CallbackContext context)
        {
        }

        public void OnSecondaryTouch(InputAction.CallbackContext context)
        {
        }
    }
}
