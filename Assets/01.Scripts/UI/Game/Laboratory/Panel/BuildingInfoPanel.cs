using JMT.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Laboratory
{
    public class BuildingInfoPanel : LaboratoryPanel
    {
        public event Action<BuildingDataSO> OnBuildingStudyEvent;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descText;
        [SerializeField] private Transform needItemContent;
        [SerializeField] private CellUI useFuel;
        [SerializeField] private Button studyButton;

        private List<CellUI> needItemList;

        private BuildingDataSO currentData;

        private void Awake()
        {
            needItemList = needItemContent.GetComponentsInChildren<CellUI>().ToList();
            studyButton.onClick.AddListener(() => OnBuildingStudyEvent?.Invoke(currentData));
        }
        
    }
}
