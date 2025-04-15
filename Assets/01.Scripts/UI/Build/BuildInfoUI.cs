using JMT.Building;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class BuildInfoUI : PanelUI
    {
        public event Action OnBuildEvent;
        private TextMeshProUGUI buildingNameText, descriptionText;
        private Button buildButton;
        
        // Bottom


        private void Awake()
        {
            buildingNameText = PanelTrm.Find("NameTxt").GetComponent<TextMeshProUGUI>();
            descriptionText = PanelTrm.Find("Description").GetComponentInChildren<TextMeshProUGUI>();
            buildButton = PanelTrm.Find("BuildBtn").GetComponent<Button>();
            buildButton.onClick.AddListener(() => OnBuildEvent?.Invoke());
        }

        public override void OpenUI()
        {
            panelGroup.alpha = 1f;

            panelGroup.blocksRaycasts = true;
            panelGroup.interactable = true;
        }

        public void SetInfo(BuildingDataSO data)
        {
            buildingNameText.text = data.buildingName;
            descriptionText.text = data.buildingDescription;
        }
    }
}
