using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class BuildingUI : PanelUI
    {
        private Button exitButton, createButton, workerButton, upgradeButton;
        private CreateUI createUI;
        private ManageUI manageUI;
        private UpgradeUI upgradeUI;
        private PanelUI currentPanel;

        private void Awake()
        {   
            Transform buttonGroup = PanelTrm.Find("Panel").Find("ButtonGroup");
            exitButton = buttonGroup.Find("ExitBtn").GetComponent<Button>();
            createButton = buttonGroup.Find("CreateBtn").GetComponent<Button>();
            workerButton = buttonGroup.Find("WorkerBtn").GetComponent<Button>();
            upgradeButton = buttonGroup.Find("UpgradeBtn").GetComponent<Button>();
            exitButton.onClick.AddListener(HandleExitButton);
            createButton.onClick.AddListener(HandleCreateButton);
            workerButton.onClick.AddListener(HandleWorkerButton);
            upgradeButton.onClick.AddListener(HandleUpgradeButton);

            Transform buildingUI = transform.Find("BuildingUI");
            createUI = buildingUI.GetComponent<CreateUI>();
            manageUI = buildingUI.GetComponent<ManageUI>();
            upgradeUI = buildingUI.GetComponent<UpgradeUI>();
        }

        private void OnDestroy()
        {
            exitButton.onClick.RemoveListener(HandleExitButton);
            createButton.onClick.RemoveListener(HandleCreateButton);
            workerButton.onClick.RemoveListener(HandleWorkerButton);
            upgradeButton.onClick.RemoveListener(HandleUpgradeButton);
        }

        public override void OpenUI()
        {
            base.OpenUI();
            ChangePanel(createUI);
        }

        public override void CloseUI()
        {
            base.CloseUI();
            currentPanel?.CloseUI();
        }

        private void HandleExitButton()
        {
            CloseUI();
        }

        private void HandleCreateButton()
        {
            ChangePanel(createUI);
        }

        private void HandleWorkerButton()
        {
            ChangePanel(manageUI);
        }

        private void HandleUpgradeButton()
        {
            ChangePanel(upgradeUI);
        }

        private void ChangePanel(PanelUI panel)
        {
            currentPanel?.CloseUI();
            currentPanel = panel;
            currentPanel.OpenUI();
        }
    }
}
