using JMT.Building;
using JMT.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Laboratory
{
    public class LaboratoryView : PanelUI
    {
        public event Action<LaboratoryCategory> OnChangedCategory;
        public event Action OnExitEvent;

        [Header("Category Button")]
        [SerializeField] private Button studyCategoryButton;
        [SerializeField] private Button equipmentCategoryButton;
        [SerializeField] private Button workerCategoryButton;
        [SerializeField] private Button upgradeCategoryButton;

        [Header("Cell Content")]
        [SerializeField] private Transform cellContent;
        private List<CellUI> cellList;

        [SerializeField] private Button exitButton;

        private void Awake()
        {
            cellList = cellContent.GetComponentsInChildren<CellUI>().ToList();

            studyCategoryButton.onClick.AddListener(() => ChangeCategory(LaboratoryCategory.Study));
            equipmentCategoryButton.onClick.AddListener(() => ChangeCategory(LaboratoryCategory.Equipment));
            workerCategoryButton.onClick.AddListener(() => ChangeCategory(LaboratoryCategory.Worker));
            upgradeCategoryButton.onClick.AddListener(() => ChangeCategory(LaboratoryCategory.Upgrade));
            exitButton.onClick.AddListener(() => OnExitEvent?.Invoke());
        }

        private void OnDestroy()
        {
            studyCategoryButton.onClick.RemoveAllListeners();
            equipmentCategoryButton.onClick.RemoveAllListeners();
            workerCategoryButton.onClick.RemoveAllListeners();
            upgradeCategoryButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }

        public void SetCell(List<BuildingDataSO> list, Action<BuildingDataSO> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int value = i;
                cellList[value].SetCell(list[value]);
                cellList[value].CellButton?.onClick.AddListener(() => action(list[value]));
            }
        }

        public void SetCell(List<ToolSO> list, Action<ToolSO> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int value = i;
                cellList[value].SetCell(list[value]);
                cellList[value].CellButton?.onClick.AddListener(() => action(list[value]));
            }
        }

        public void ResetCell()
        {
            for (int i = 0; i < cellList.Count; i++)
            {
                cellList[i].ResetCell();
                cellList[i].CellButton?.onClick.RemoveAllListeners();
            }
        }

        private void ChangeCategory(LaboratoryCategory category)
        {
            OnChangedCategory?.Invoke(category);
        }

        
    }
}
