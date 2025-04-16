using JMT.Building;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    [System.Serializable]
    public struct ButtonWithIcon
    {
        public Button button;
        public Image icon;
    }

    public class ConstructUI : PanelUI
    {
        [SerializeField] private BuildInfoUI infoUI;
        [SerializeField] private Transform scrollView;
        [SerializeField] private PVCBuilding pvcObject;
        [SerializeField] private Button exitButton;
        [SerializeField] private AnimationColor buttonColor, iconColor;

        private List<CellUI> cells = new();
        private List<BuildingDataSO> buildingDatas;
        private ButtonWithIcon itemCategory, facilityCategory, defenseCategory;

        private bool isBuild;

        private void Awake()
        {
            buildingDatas = new List<BuildingDataSO>();

            cells = scrollView.Find("Viewport").Find("Content").GetComponentsInChildren<CellUI>().ToList();

            Transform category = scrollView.Find("Category");
            itemCategory.button = category.Find("ItemCategoryBtn").GetComponent<Button>();
            facilityCategory.button = category.Find("FacilityCategoryBtn").GetComponent<Button>();
            defenseCategory.button = category.Find("DefenseCategoryBtn").GetComponent<Button>();
            itemCategory.icon = itemCategory.button.transform.Find("Icon").GetComponent<Image>();
            facilityCategory.icon = facilityCategory.button.transform.Find("Icon").GetComponent<Image>();
            defenseCategory.icon = defenseCategory.button.transform.Find("Icon").GetComponent<Image>();

            infoUI.OnBuildEvent += HandleBuildButton;
            exitButton.onClick.AddListener(CloseUI);
            itemCategory.button.onClick.AddListener(() =>
            {
                SelectCategory(BuildingCategory.ItemBuilding);
                SetButtonColor(0);
            });
            facilityCategory.button.onClick.AddListener(() =>
            {
                SelectCategory(BuildingCategory.FacilityBuilding);
                SetButtonColor(1);
            });
            defenseCategory.button.onClick.AddListener(() =>
            {
                SelectCategory(BuildingCategory.DefenseBuilding);
                SetButtonColor(2);
            });
        }


        public override void OpenUI()
        {
            SelectCategory(BuildingCategory.ItemBuilding);
            SetButtonColor(0);
            UIManager.Instance.GameUI.CloseUI();
            UIManager.Instance.PlayerControlActive(false);

            panelGroup.alpha = 1f;

            panelGroup.blocksRaycasts = true;
            panelGroup.interactable = true;
        }

        public override void CloseUI()
        {
            if (!isBuild)
                TileManager.Instance.CurrentTile.DestroyBuilding();
            infoUI.CloseUI();
            UIManager.Instance.GameUI.OpenUI();
            UIManager.Instance.PlayerControlActive(true);
            isBuild = false;
            base.CloseUI();
        }

        private void SelectCategory(BuildingCategory? category = null)
        {
            List<BuildingDataSO> list = BuildingManager.Instance.GetDictionary();
            if (category != null)
                list = CategorySystem.FilteringCategory(list, category);

            for (int i = 0; i < cells.Count; i++)
            {
                int value = i;

                Button cellButton = cells[value].GetComponent<Button>();
                cellButton.onClick.RemoveAllListeners();
                cells[value].ResetCell();

                if (value < list.Count)
                {
                    cells[value].SetCell(list[value].BuildingName);
                    cellButton.onClick.AddListener(() => HandleSetInfo(list[value]));
                }
            }
        }

        private void SetButtonColor(int index)
        {
            ButtonWithIcon[] categories = new[]
            {
                itemCategory,
                facilityCategory,
                defenseCategory
            };

            for (int i = 0; i < categories.Length; i++)
            {
                bool isSelected = i == index;
                buttonColor.ChangeColor(categories[i].button.image, isSelected, 0.3f);
                iconColor.ChangeColor(categories[i].icon, isSelected, 0.3f);
            }
        }

        private void HandleSetInfo(BuildingDataSO data)
        {
            BuildingManager.Instance.CurrentBuilding = data;
            if (!infoUI.IsOpen) infoUI.OpenUI();
            infoUI.SetInfo(data);
            TileManager.Instance.CurrentTile.TestBuild(BuildingManager.Instance.CurrentBuilding);
        }

        private void HandleBuildButton()
        {
            if (BuildingManager.Instance.CurrentBuilding == null)
            {
                Debug.Log("읎으요");
                return;
            }
            if (!InventoryManager.Instance.CalculateItem(BuildingManager.Instance.CurrentBuilding.NeedItems)) return;
            CloseUI();
            isBuild = true;
            TileManager.Instance.CurrentTile.EdgeEnable(false);
            TileManager.Instance.CurrentTile.Build(BuildingManager.Instance.CurrentBuilding, pvcObject);
            buildingDatas.Add(BuildingManager.Instance.CurrentBuilding);
            UIManager.Instance.WorkUI.SetBuilding(buildingDatas);
        }
    }
}
