using JMT.Building;
using JMT.Planets.Tile;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class StationUI : PanelUI
    {
        private Button exitButton, createButton, upgradeButton;

        private StationUpgradeUI upgradeUI;
        private PanelUI currentPanel;

        private int level = 0;

        private void Awake()
        {
            Transform buttonGroup = PanelTrm.Find("Panel").Find("ButtonGroup");
            exitButton = buttonGroup.Find("ExitBtn").GetComponent<Button>();
            //createButton = buttonGroup.Find("CreateBtn").GetComponent<Button>();
            upgradeButton = buttonGroup.Find("UpgradeBtn").GetComponent<Button>();
            exitButton.onClick.AddListener(HandleExitButton);
            //createButton.onClick.AddListener(HandleCreateButton);
            upgradeButton.onClick.AddListener(HandleUpgradeButton);

            Transform buildingUI = transform.Find("BuildingUI");
        }

        public override void OpenUI()
        {
            base.OpenUI();
            //ChangePanel(createUI);
        }

        private void HandleExitButton()
        {
            CloseUI();
        }

        private void HandleWorkerButton()
        {
            //ChangePanel(manageUI);
        }

        private void HandleUpgradeButton()
        {

            //ChangePanel(upgradeUI);
        }

        private void ChangePanel(PanelUI panel)
        {
            currentPanel?.CloseUI();
            currentPanel = panel;
            currentPanel.OpenUI();
        }
    }
}
