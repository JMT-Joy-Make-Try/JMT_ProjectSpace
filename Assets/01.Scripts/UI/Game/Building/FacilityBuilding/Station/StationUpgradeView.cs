using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class StationUpgradeView : PanelUI
    {
        public event Action OnUpgradeEvent;

        [SerializeField] private Button upgradeButton;
        private int level = 0;

        private void Awake()
        {
            upgradeButton.onClick.AddListener(HandleUpgradeButton);
        }

        private void OnDestroy()
        {
            upgradeButton.onClick.RemoveListener(HandleUpgradeButton);
        }

        private void HandleUpgradeButton()
        {
            level++;
            OnUpgradeEvent?.Invoke();
        }
    }
}
