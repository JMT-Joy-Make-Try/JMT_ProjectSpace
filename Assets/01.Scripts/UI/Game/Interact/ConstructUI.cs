using JMT.Building;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class ConstructUI : PanelUI
    {
        [SerializeField] private BuildInfoUI infoUI;
        [SerializeField] private Transform scrollView;
        [SerializeField] private PVCBuilding pvcObject;
        [SerializeField] private Button exitButton;

        private List<CellUI> cells = new();
        private List<BuildingDataSO> buildingDatas;
        private Button itemButton, facilityButton, defenseButton;

        private bool isBuild;

        private void Awake()
        {
            buildingDatas = new List<BuildingDataSO>();

            cells = scrollView.Find("Viewport").Find("Content").GetComponentsInChildren<CellUI>().ToList();

            Transform category = scrollView.Find("Category");
            itemButton = category.Find("ItemCategoryBtn").GetComponent<Button>();
            facilityButton = category.Find("FacilityCategoryBtn").GetComponent<Button>();
            defenseButton = category.Find("DefenseCategoryBtn").GetComponent<Button>();

            infoUI.OnBuildEvent += HandleBuildButton;
            exitButton.onClick.AddListener(CloseUI);
            itemButton.onClick.AddListener(() => SelectCategory(BuildingCategory.ItemBuilding));
            facilityButton.onClick.AddListener(() => SelectCategory(BuildingCategory.FacilityBuilding));
            defenseButton.onClick.AddListener(() => SelectCategory(BuildingCategory.DefenseBuilding));
        }


        public override void OpenUI()
        {
            SelectCategory();
            UIManager.Instance.GameUI.CloseUI();

            panelGroup.alpha = 1f;

            panelGroup.blocksRaycasts = true;
            panelGroup.interactable = true;

            Time.timeScale = 0;
        }

        public override void CloseUI()
        {
            if (!isBuild)
                TileManager.Instance.CurrentTile.DestroyBuilding();
            infoUI.CloseUI();
            UIManager.Instance.GameUI.OpenUI();
            isBuild = false;
            base.CloseUI();
        }

        private void SelectCategory(BuildingCategory? category = null)
        {
            List<BuildingDataSO> list = BuildingManager.Instance.GetDictionary();
            int falseValue = 0;

            for (int i = 0; i < cells.Count; i++)
            {
                int value = i;
                cells[value].GetComponent<Button>().onClick.RemoveAllListeners();
                cells[i].ResetCell();
                if (i < list.Count)
                {
                    var pair = list[i];
                    if (category == null || category.Value.Equals(pair.Category))
                    {
                        cells[value - falseValue].SetCell(list[value].BuildingName);
                        cells[value - falseValue].GetComponent<Button>().onClick.AddListener(() => HandleSetInfo(pair));
                    }
                    else falseValue++;
                }
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
