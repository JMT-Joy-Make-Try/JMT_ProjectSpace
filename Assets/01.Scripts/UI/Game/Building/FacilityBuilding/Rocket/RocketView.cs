using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Rocket
{
    public class RocketView : PanelUI
    {
        public event Action OnUpgradeEvent;
        public event Action OnExitEvent;

        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            upgradeButton.onClick.AddListener(HandleUpgradeButton);
            exitButton.onClick.AddListener(HandleExitButton);
        }

        private void OnDestroy()
        {
            upgradeButton.onClick.RemoveListener(HandleUpgradeButton);
            exitButton.onClick.RemoveListener(HandleExitButton);
        }

        private void HandleUpgradeButton()
        {
            OnUpgradeEvent?.Invoke();
        }

        private void HandleExitButton()
        {
            OnExitEvent?.Invoke();
        }
    }
}
