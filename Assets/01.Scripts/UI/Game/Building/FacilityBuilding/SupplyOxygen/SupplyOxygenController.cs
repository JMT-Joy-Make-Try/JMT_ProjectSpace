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
        [SerializeField] private SidePanelUI workItemPanel;

        private PanelUI currentPanel;

        private void Awake()
        {
            view.OnWorkButtonEvent += HandleWorkEvent;
            view.OnWorkerButtonEvent += HandleWorkerEvent;
            view.OnUpgradeButtonEvent += HandleUpgradeEvent;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OpenPanel();
            if (Input.GetKeyDown(KeyCode.Tab))
                ClosePanel();
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
            view.CloseUI();
            GameUIManager.Instance.GameUICompo.OpenUI();
            GameUIManager.Instance.PlayerControlActive(true);
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
            workItemPanel.CloseUI();
            currentPanel?.CloseUI();
            currentPanel = panel;
            currentPanel?.OpenUI();
        }
    }
}
