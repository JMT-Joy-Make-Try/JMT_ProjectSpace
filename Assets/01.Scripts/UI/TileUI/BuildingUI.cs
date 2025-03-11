using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class BuildingUI : PanelUI
    {
        private Button upgradeButton, setPeopleButton;

        private void Awake()
        {
            Transform buttonContainer = PanelTrm.Find("ButtonContainer");
            upgradeButton = buttonContainer.Find("UpgradeBtn").GetComponent<Button>();
            setPeopleButton = buttonContainer.Find("SetPeopleBtn").GetComponent<Button>();

            upgradeButton.onClick.AddListener(HandleUpgradeButton);
            setPeopleButton.onClick.AddListener(HandleSetPeopleButton);
        }

        private void HandleUpgradeButton()
        {
            Debug.Log("Click Upgrade Button");
            // 건물 업그레이드
        }

        private void HandleSetPeopleButton()
        {
            Debug.Log("Click Set People Button");
        }

    }
}
