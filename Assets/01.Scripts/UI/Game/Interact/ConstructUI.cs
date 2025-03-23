using JMT.Building;
using JMT.Core.Manager;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class ConstructUI : PanelUI
    {
        [SerializeField] private Transform content;
        private List<BuildCellUI> cells = new();

        private TextMeshProUGUI nameText, needItemText;
        private Button buildButton;
        
        private List<BuildingDataSO> buildingDatas;

        private void Awake()
        {
            cells = content.GetComponentsInChildren<BuildCellUI>().ToList();

            Transform panelRight = PanelTrm.Find("Panel").Find("Right");
            nameText = panelRight.Find("Preview").GetComponentInChildren<TextMeshProUGUI>();
            needItemText = panelRight.Find("Build").Find("NeedItem").GetComponentInChildren<TextMeshProUGUI>();
            buildButton = panelRight.Find("Build").GetComponentInChildren<Button>();
            buildButton.onClick.AddListener(HandleBuildButton);
            
            buildingDatas = new List<BuildingDataSO>();
        }

        public override void OpenUI()
        {
            TotalCategory();
            base.OpenUI();
        }

        private void TotalCategory()
        {
            List<BuildingDataSO> list = BuildingManager.Instance.GetDictionary();

            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].SetItemCell(string.Empty);
                if (i < list.Count)
                {
                    cells[i].SetItemCell(list[i].name);
                    int index = i; // 현재 i 값을 저장하여 람다 함수 내에서 사용
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
                    int index = i; // 현재 i 값을 저장하여 람다 함수 내에서 사용
                    cells[i].GetComponent<Button>().onClick.AddListener(() => HandleSetInfo(list[index]));
                }
            }
        }

        private void HandleSetInfo(BuildingDataSO data)
        {
            BuildingManager.Instance.CurrentBuilding = data;
            nameText.text = data.name;
            needItemText.text = string.Empty;
            foreach (var needItem in data.needItems)
            {
                needItemText.text += ObjectExtension.GetName(needItem.Key) + " - " + needItem.Value + "\n";
            }
        }

        private void HandleBuildButton()
        {
            if(BuildingManager.Instance.CurrentBuilding == null)
            {
                Debug.Log("읎으요");
                return;
            }
            if (!InventoryManager.Instance.CalculateItem(BuildingManager.Instance.CurrentBuilding.needItems)) return;
            TileManager.Instance.CurrentTile.EdgeEnable(false);
            TileManager.Instance.CurrentTile.Build(BuildingManager.Instance.CurrentBuilding);
            buildingDatas.Add(BuildingManager.Instance.CurrentBuilding);
            UIManager.Instance.WorkUI.SetBuilding(buildingDatas);
            CloseUI();
        }
    }
}
