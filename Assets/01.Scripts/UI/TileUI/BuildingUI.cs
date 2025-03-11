using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JMT.UISystem
{
    public class BuildingUI : PanelUI
    {
        private void HandleUpgradeButton(ClickEvent evt)
        {
            Debug.Log("Click Upgrade Button");
            // 건물 업그레이드
        }

        private void HandleSetPeopleButton(ClickEvent evt)
        {
            Debug.Log("Click Set People Button");
        }

    }
}
