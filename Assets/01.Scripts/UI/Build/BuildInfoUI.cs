using JMT.Building;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<CellUI> needItemList;
        private CellUI useFuel;

        private void Awake()
        {
            buildingNameText = PanelTrm.Find("NameTxt").GetComponent<TextMeshProUGUI>();
            descriptionText = PanelTrm.Find("Description").GetComponentInChildren<TextMeshProUGUI>();
            buildButton = PanelTrm.Find("BuildBtn").GetComponent<Button>();

            Transform bottom = PanelTrm.Find("Bottom");
            needItemList = bottom.Find("NeedItemList").GetComponentsInChildren<CellUI>().ToList();
            useFuel = bottom.Find("UseFuel").GetComponent<CellUI>();

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
            for(int i = 0; i < needItemList.Count; i++)
            {
                int value = i;
                var needItems = data.needItems.ToList();
                if (needItems.Count > value)
                {
                    needItemList[value].SetCell(needItems[value].Key, "X" + needItems[value].Value.ToString());
                }
                else
                    needItemList[value].ResetCell();
            }
            useFuel.SetCell(string.Empty, data.useFuelPerSecond.ToString("F1"));
        }
    }
}
