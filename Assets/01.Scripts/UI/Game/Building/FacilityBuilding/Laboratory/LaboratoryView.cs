using JMT.Building;
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

        private List<Action> cellButtonHandlers = new();

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

        public void SetCell<T>(List<T> list, Action<T> action) where T : ICellDisplayData
        {
            ResetCell();
            for (int i = 0; i < list.Count; i++)
            {
                int value = i;
                cellButtonHandlers.Add(() => action(list[value]));
                cellList[value].SetCell(list[value]);
                cellList[value].OnClickCellEvent += cellButtonHandlers[value];
            }
        }

        public void ResetCell()
        {
            for (int i = 0; i < cellList.Count; i++)
            {
                cellList[i].ResetCell();
                cellList[i].OnClickCellEvent -= cellButtonHandlers[i];
            }
        }

        private void ChangeCategory(LaboratoryCategory category)
        {
            OnChangedCategory?.Invoke(category);
        }
    }
}
