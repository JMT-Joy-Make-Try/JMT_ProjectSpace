using JMT.Building;
using JMT.Planets.Tile;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class StationUpgradeUI : PanelUI
    {
        private Button upgradeButton;
        private int level = 0;

        private void Awake()
        {
            upgradeButton = PanelTrm.Find("Right").GetComponentInChildren<Button>();
            upgradeButton.onClick.AddListener(HandleUpgradeButton);
        }

        private void HandleUpgradeButton()
        {
            TileManager.Instance.GetInteraction().GetComponentInChildren<BaseBuilding>().FixStation();
            Debug.Log("그냥 레벨업할게요.");
            level++;
            UIManager.Instance.StationUI.CloseUI();
        }
    }
}
