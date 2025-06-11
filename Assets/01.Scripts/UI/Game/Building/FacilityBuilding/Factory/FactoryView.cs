using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Factory
{
    public class FactoryView : PanelUI
    {
        public event Action OnExitButtonEvent;
        public event Action OnItemButtonEvent;
        public event Action OnToolButtonEvent;

        [SerializeField] private Button itemButton;
        [SerializeField] private Button toolButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            itemButton.onClick.AddListener(HandleItemButton);
            toolButton.onClick.AddListener(HandleToolButton);
            exitButton.onClick.AddListener(HandleExitButton);
        }

        private void HandleItemButton()
        {
            OnItemButtonEvent?.Invoke();
        }

        private void HandleToolButton()
        {
            OnToolButtonEvent?.Invoke();
        }

        private void HandleExitButton()
        {
            OnExitButtonEvent?.Invoke();
        }
    }
}
