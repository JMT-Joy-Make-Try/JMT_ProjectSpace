using JMT.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Interact
{
    public class BuildInfoView : PanelUI
    {
        public event Action OnBuildEvent;

        [SerializeField] private TextMeshProUGUI buildingNameText, descriptionText;
        [SerializeField] private Button buildButton;
        [SerializeField] private Transform bottom;
        [SerializeField] private CellUI useFuel;

        private List<CellUI> needItemList = new();

        private void Awake()
        {
            needItemList = bottom.Find("NeedItemList").GetComponentsInChildren<CellUI>().ToList();
            buildButton.onClick.AddListener(() => OnBuildEvent?.Invoke());
        }

        private void OnDestroy()
        {
            buildButton.onClick.RemoveAllListeners();
        }

        public void SetInfo(BuildingDataSO data)
        {
            if (!IsOpen) OpenUI();

            buildingNameText.text = data.BuildingName;
            descriptionText.text = data.BuildingDescription;
            for(int i = 0; i < needItemList.Count; i++)
            {
                int value = i;
                var needItems = data.buildingLevel[0].NeedItems.ToList();
                if (needItems.Count > value)
                {
                    needItemList[value].SetCell(needItems[value].Key, "X" + needItems[value].Value.ToString());
                }
                else
                    needItemList[value].ResetCell();
            }
            useFuel.SetCell(string.Empty, data.buildingLevel[0].UseFuelPerSecond.ToString("F1"));
        }
    }
}
