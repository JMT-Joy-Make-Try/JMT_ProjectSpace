using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class StationView : PanelUI
    {
        public event Action OnStorageButtonEvent;
        public event Action OnUpgradeButtonEvent;
        public event Action OnExitButtonEvent;

        [SerializeField] private Button storageButton, upgradeButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            storageButton.onClick.AddListener(HandleStorageButton);
            upgradeButton.onClick.AddListener(HandleUpgradeButton);
            exitButton.onClick.AddListener(HandleExitButton);
        }

        private void OnDestroy()
        {
            storageButton.onClick.RemoveListener(HandleStorageButton);
            upgradeButton.onClick.RemoveListener(HandleUpgradeButton);
            exitButton.onClick.RemoveListener(HandleExitButton);
        }

        private void HandleStorageButton()
            => OnStorageButtonEvent?.Invoke();

        private void HandleUpgradeButton()
            => OnUpgradeButtonEvent?.Invoke();

        private void HandleExitButton()
            => OnExitButtonEvent?.Invoke();

    }
}
