using JMT.Building;
using JMT.Core.Manager;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class ConstructUI : PanelUI
    {
        [SerializeField] private BuildInfoUI infoUI;
        [SerializeField] private Transform content;
        [SerializeField] private PVCBuilding pvcObject;
        [SerializeField] private Button exitButton;

        private List<BuildCellUI> cells = new();
        private List<BuildingDataSO> buildingDatas;

        private bool isBuild;

        private void Awake()
        {
            cells = content.GetComponentsInChildren<BuildCellUI>().ToList();

            buildingDatas = new List<BuildingDataSO>();

            infoUI.OnBuildEvent += HandleBuildButton;
            exitButton.onClick.AddListener(CloseUI);
        }


        public override void OpenUI()
        {
            TotalCategory();
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

        private void TotalCategory()
        {
            List<BuildingDataSO> list = BuildingManager.Instance.GetDictionary();

            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].SetItemCell(string.Empty);
                if (i < list.Count)
                {
                    cells[i].SetItemCell(list[i].buildingName);
                    int index = i;
                    cells[i].GetComponent<Button>().onClick.AddListener(() => HandleSetInfo(list[index]));
                }
            }
        }

        private void SelectCategory(BuildingCategory category)
        {
            cells.Clear();
            List<BuildingDataSO> list = BuildingManager.Instance.GetDictionary();

            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].SetItemCell(string.Empty);
                if (i < list.Count)
                {
                    if (category != list[i].category) continue;
                    cells[i].SetItemCell(list[i].name);
                    int index = i;
                    cells[i].GetComponent<Button>().onClick.AddListener(() => HandleSetInfo(list[index]));
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
            if(BuildingManager.Instance.CurrentBuilding == null)
            {
                Debug.Log("읎으요");
                return;
            }
            if (!InventoryManager.Instance.CalculateItem(BuildingManager.Instance.CurrentBuilding.needItems)) return;
            CloseUI();
            isBuild = true;
            TileManager.Instance.CurrentTile.EdgeEnable(false);
            TileManager.Instance.CurrentTile.Build(BuildingManager.Instance.CurrentBuilding, pvcObject);
            buildingDatas.Add(BuildingManager.Instance.CurrentBuilding);
            UIManager.Instance.WorkUI.SetBuilding(buildingDatas);
        }
    }
}
