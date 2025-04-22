using JMT.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Interact
{
    [Serializable]
    public struct IconButton
    {
        public Button button;
        public Image icon;
    }

    public class ConstructView : PanelUI
    {
        public event Action<BuildingDataSO> OnInfoEvent;
        public event Action<BuildingCategory> OnCategoryChangedEvent;

        [SerializeField] private AnimationColor buttonColor, iconColor;
        [SerializeField] private Transform cellParent;
        [SerializeField] private IconButton itemCategory, facilityCategory, defenseCategory;

        private List<CellUI> cells = new();
        
        private void Awake()
        {
            cells = cellParent.GetComponentsInChildren<CellUI>().ToList();
            itemCategory.button.onClick.AddListener
                (() => OnCategoryChangedEvent?.Invoke(BuildingCategory.ItemBuilding));
            facilityCategory.button.onClick.AddListener
                (() => OnCategoryChangedEvent?.Invoke(BuildingCategory.FacilityBuilding));
            defenseCategory.button.onClick.AddListener
                (() => OnCategoryChangedEvent?.Invoke(BuildingCategory.DefenseBuilding));
        }

        private void OnDestroy()
        {
            itemCategory.button.onClick.RemoveAllListeners();
            facilityCategory.button.onClick.RemoveAllListeners();
            defenseCategory.button.onClick.RemoveAllListeners();
        }


        public override void OpenUI()
        {
            panelGroup.alpha = 1f;
            panelGroup.blocksRaycasts = true;
            panelGroup.interactable = true;
        }

        public void ChangeCell(List<BuildingDataSO> list)
        {
            ResetCell();
            for (int i = 0; i < list.Count; i++)
            {
                int value = i;
                Button cellButton = cells[value].GetComponent<Button>();

                if (value < list.Count)
                {
                    cells[value].SetCell(list[value].BuildingName);
                    cellButton.onClick.AddListener(() => OnInfoEvent?.Invoke(list[value]));
                }
            }
        }

        private void ResetCell()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                int value = i;

                Button cellButton = cells[value].GetComponent<Button>();
                cellButton.onClick.RemoveAllListeners();
                cells[value].ResetCell();
            }
        }

        public void SetButtonColor(int index)
        {
            IconButton[] categories = new[]
            {
                itemCategory,
                defenseCategory,
                facilityCategory,
            };

            for (int i = 0; i < categories.Length; i++)
            {
                bool isSelected = i == index;
                buttonColor.ChangeColor(categories[i].button.image, isSelected, 0.3f);
                iconColor.ChangeColor(categories[i].icon, isSelected, 0.3f);
            }
        }
    }
}
