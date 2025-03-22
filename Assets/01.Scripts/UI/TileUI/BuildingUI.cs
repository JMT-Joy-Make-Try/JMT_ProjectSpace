using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class BuildingUI : PanelUI
    {
        private Button upgradeButton, createButton, manageButton;

        private void Awake()
        {
            Transform buttonContainer = PanelTrm.Find("ButtonContainer");
            upgradeButton = buttonContainer.Find("UpgradeBtn").GetComponent<Button>();
            createButton = buttonContainer.Find("CreateBtn").GetComponent<Button>();
            manageButton = buttonContainer.Find("ManageBtn").GetComponent<Button>();

            upgradeButton.onClick.AddListener(HandleUpgradeButton);
            createButton.onClick.AddListener(HandleCreateButton);
            manageButton.onClick.AddListener(HandleManageButton);
        }

        public override void OpenUI()
        {
            //UIManager.Instance.NoTouchUI.NoTouchZone.OnClickEvent += CloseUI;
            base.OpenUI();
        }

        private void HandleUpgradeButton()
        {
            Debug.Log("Click Upgrade Button");
            // 건물 업그레이드
            UIManager.Instance.UpgradeUI.OpenUI();
        }

        private void HandleCreateButton()
        {
            Debug.Log("Click Manage Button");
            UIManager.Instance.CreateUI.OpenUI();
        }

        private void HandleManageButton()
        {
            Debug.Log("Click Set People Button");
            UIManager.Instance.ManageUI.OpenUI();
        }

    }
}
