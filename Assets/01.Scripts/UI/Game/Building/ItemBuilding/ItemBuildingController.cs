using UnityEngine;

namespace JMT.UISystem.Building
{
    public class ItemBuildingController : MonoBehaviour
    {
        [SerializeField] private ItemBuildingView view;
        private ItemBuildingModel model = new();

        [Header("PanelUI")]
        [SerializeField] private CreateUI createUI;
        [SerializeField] private ManageController manageUI;
        [SerializeField] private UpgradeUI upgradeUI;
        private PanelUI currentPanel;

        private void Awake()
        {
            view.OnExitButtonClicked += CloseUI;
            view.OnCreateButtonClicked += HandleCreateButton;
            view.OnWorkerButtonClicked += HandleWorkerButton;
            view.OnUpgradeButtonClicked += HandleUpgradeButton;
        }

        private void OnDestroy()
        {
            view.OnExitButtonClicked -= CloseUI;
            view.OnCreateButtonClicked -= HandleCreateButton;
            view.OnWorkerButtonClicked -= HandleWorkerButton;
            view.OnUpgradeButtonClicked -= HandleUpgradeButton;
        }

        public void OpenUI()
        {
            view.OpenUI();
            ChangePanel(createUI);
        }

        public void CloseUI()
        {
            view.CloseUI();
            currentPanel?.CloseUI();
        }

        private void HandleCreateButton() => ChangePanel(createUI);

        private void HandleWorkerButton() => ChangePanel(manageUI);

        private void HandleUpgradeButton() => ChangePanel(upgradeUI);

        private void ChangePanel(PanelUI panel = null)
        {
            currentPanel?.CloseUI();
            if (panel == null) return;
            currentPanel = panel;
            currentPanel.OpenUI();
        }
    }
}
