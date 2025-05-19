using JMT.Item;
using System;
using UnityEngine;

namespace JMT.UISystem.SupplyOxygen
{
    public class SupplyOxygenController : MonoBehaviour
    {
        [SerializeField] private SupplyOxygenView view;
        [SerializeField] private OxygenWorkView workView;
        [SerializeField] private OxygenWorkerView workerView;
        [SerializeField] private OxygenUpgradeView upgradeView;

        private PanelUI currentPanel;

        private void Awake()
        {
            view.OnWorkButtonEvent += HandleWorkEvent;
            view.OnWorkerButtonEvent += HandleWorkerEvent;
            view.OnUpgradeButtonEvent += HandleUpgradeEvent;
            view.OnExitButtonEvent += ClosePanel;
        }

        public void OpenPanel()
        {
            view.OpenUI();
            GameUIManager.Instance.GameUICompo.CloseUI();
            GameUIManager.Instance.PlayerControlActive(false);
            SetCurrentPanel(workView);
        }

        public void ClosePanel()
        {
            GameUIManager.Instance.GameUICompo.OpenUI();
            GameUIManager.Instance.PlayerControlActive(true);
            view.CloseUI();
            SetCurrentPanel(null);
        }

        private void HandleWorkEvent()
        {
            SetCurrentPanel(workView);
        }

        private void HandleWorkerEvent()
        {
            SetCurrentPanel(workerView);
        }

        private void HandleUpgradeEvent()
        {
            SetCurrentPanel(upgradeView);
        }

        public void SetCurrentPanel(PanelUI panel)
        {
            //if (currentPanel == panel) return;
            currentPanel?.CloseUI();
            currentPanel = panel;
            currentPanel?.OpenUI();
        }
    }
}
