using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.SupplyOxygen
{
    public class SupplyOxygenView : PanelUI
    {
        public event Action OnWorkButtonEvent;
        public event Action OnWorkerButtonEvent;
        public event Action OnUpgradeButtonEvent;
        public event Action OnExitButtonEvent;

        [SerializeField] private Button workButton, workerButton, upgradeButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            workButton.onClick.AddListener(HandleWorkButton);
            workerButton.onClick.AddListener(HandleWorkerButton);
            upgradeButton.onClick.AddListener(HandleUpgradeButton);
            exitButton.onClick.AddListener(HandleExitButton);
        }

        private void OnDestroy()
        {
            workButton.onClick.RemoveListener(HandleWorkButton);
            workerButton.onClick.RemoveListener(HandleWorkerButton);
            upgradeButton.onClick.RemoveListener(HandleUpgradeButton);
            exitButton.onClick.AddListener(HandleExitButton);
        }

        private void HandleExitButton()
        {
            OnExitButtonEvent?.Invoke();
        }

        private void HandleWorkButton()
        {
            OnWorkButtonEvent?.Invoke();
        }

        private void HandleWorkerButton()
        {
            OnWorkerButtonEvent?.Invoke();
        }

        private void HandleUpgradeButton()
        {
            OnUpgradeButtonEvent?.Invoke();
        }
    }
}
