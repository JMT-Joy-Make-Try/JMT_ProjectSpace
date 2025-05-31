using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JMT
{
    [CreateAssetMenu(menuName = "SO/Input/TitleInputSO")]
    public class TitleInputSO : ScriptableObject, Controls.IScreenTouchActions
    {
        private Controls controls;
        public event Action OnTouchEvent;

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

        public void OnPrimaryTouch(InputAction.CallbackContext context)
        {
        }

        public void OnPrimaryTouchContact(InputAction.CallbackContext context)
        {
            if (context.started)
                OnTouchEvent?.Invoke();
        }

        public void OnSecondaryTouch(InputAction.CallbackContext context)
        {
        }

        public void OnSecondaryTouchContact(InputAction.CallbackContext context)
        {
        }
    }
}
