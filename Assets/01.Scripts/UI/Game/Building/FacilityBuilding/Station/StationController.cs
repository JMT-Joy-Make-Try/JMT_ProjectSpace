using JMT.Building;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT.UISystem.Building
{
    public class StationController : MonoBehaviour
    {
        [SerializeField] private StationView view;
        [SerializeField] private StationUpgradeView upgradeView;

        private PanelUI currentPanel;

        private void Awake()
        {
            view.OnExitButtonEvent += CloseUI;
            upgradeView.OnUpgradeEvent += HandleUpgradeButton;
        }

        private void OnDestroy()
        {
            view.OnExitButtonEvent -= CloseUI;
            upgradeView.OnUpgradeEvent -= HandleUpgradeButton;
        }

        public void OpenUI()
        {
            view.OpenUI();
            upgradeView.OpenUI();
        }

        private void CloseUI()
        {
            upgradeView.CloseUI();
            view.CloseUI();
        }

        private void HandleUpgradeButton()
        {
            TileManager.Instance.GetInteraction().GetComponentInChildren<BaseBuilding>().FixStation();
            Debug.Log("그냥 레벨업할게요.");
            CloseUI();
        }

        private void ChangePanel(PanelUI panel = null)
        {
            currentPanel?.CloseUI();
            if (panel == null) return;
            currentPanel = panel;
            currentPanel.OpenUI();
        }
    }
}
