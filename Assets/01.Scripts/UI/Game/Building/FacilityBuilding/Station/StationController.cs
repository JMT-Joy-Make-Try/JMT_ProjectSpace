using JMT.Building;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT.UISystem.Station
{
    public class StationController : MonoBehaviour
    {
        [SerializeField] private StationView view;
        [SerializeField] private StationStorageController storage;

        [Header("Upgrade Settings")]
        [SerializeField] private StationUpgradeView upgradeView;

        private IOpenablePanel currentPanel;

        private void Awake()
        {
            storage.OnEndEvent += ClosePanel;

            view.OnStorageButtonEvent += HandleStorageEvent;
            view.OnUpgradeButtonEvent += HandleUpgradeEvent;
            view.OnExitButtonEvent += ClosePanel;

            upgradeView.OnUpgradeEvent += HandleUpgradeButton;
        }

        private void OnDestroy()
        {
            view.OnExitButtonEvent -= ClosePanel;
            upgradeView.OnUpgradeEvent -= HandleUpgradeButton;
        }

        public void OpenPanel()
        {
            view.OpenUI();
            GameUIManager.Instance.GameUICompo.ClosePanel();
            GameUIManager.Instance.PlayerControlActive(false);
            ChangePanel(storage);
        }

        private void ClosePanel()
        {
            ChangePanel(null);
            GameUIManager.Instance.GameUICompo.OpenPanel();
            GameUIManager.Instance.PlayerControlActive(true);
            view.CloseUI();
        }

        private void HandleStorageEvent()
        {
            ChangePanel(storage);
        }

        private void HandleUpgradeEvent()
        {
            ChangePanel(upgradeView);
        }

        private void HandleUpgradeButton()
        {
            TileManager.Instance.GetInteraction().GetComponentInChildren<BaseBuilding>().FixStation();
            Debug.Log("그냥 레벨업할게요.");
            ClosePanel();
        }

        private void ChangePanel(IOpenablePanel panel = null)
        {
            currentPanel?.CloseUI();
            if (panel == null) return;
            currentPanel = panel;
            currentPanel.OpenUI();
        }
    }
}
