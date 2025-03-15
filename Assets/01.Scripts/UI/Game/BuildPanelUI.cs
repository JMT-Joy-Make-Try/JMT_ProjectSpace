using JMT.Building;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class BuildPanelUI : PanelUI
    {
        [SerializeField] private Transform content;
        private List<BuildCellUI> cells = new();

        private TextMeshProUGUI nameText, needItemText;
        private Button buildButton;

        private void Awake()
        {
            cells = content.GetComponentsInChildren<BuildCellUI>().ToList();

            Transform panelRight = PanelTrm.Find("Panel").Find("Right");
            nameText = panelRight.Find("Preview").GetComponentInChildren<TextMeshProUGUI>();
            needItemText = panelRight.Find("Build").Find("NeedItem").GetComponentInChildren<TextMeshProUGUI>();
            buildButton = panelRight.Find("Build").GetComponentInChildren<Button>();
            buildButton.onClick.AddListener(HandleBuildButton);
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
                needItemText.text += GetName(needItem.Key) + " - " + needItem.Value + "\n";
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
            BuildingBase b = BuildingManager.Instance.CurrentBuilding.prefab;
            TileManager.Instance._currentTile.Build(b);
            CloseUI();
        }


        private string GetName(ItemType key)
        {
            switch (key)
            {
                case ItemType.LiquidFuel:
                    return "Liquid Fuel";
                case ItemType.OrganicMatter:
                    return "Organic Matter";
                case ItemType.SpaceDust:
                    return "Space Dust";
                case ItemType.FlameIron:
                    return "Flame Iron";
                case ItemType.IceIron:
                    return "Ice Iron";
                case ItemType.Techron:
                    return "Techron";
                case ItemType.DustBundle:
                    return "Dust Bundle";
                case ItemType.DustBoard:
                    return "Dust Board";
                case ItemType.DustSteelPlate:
                    return "Dust Steel Plate";
                case ItemType.FuelConcentrate:
                    return "Fuel Concentrate";
                case ItemType.ImpureWater:
                    return "Impure Water";
                case ItemType.PureWater:
                    return "PureWater";
                case ItemType.SpaceBrick:
                    return "Space Brick";
                case ItemType.Cloth:
                    return "Cloth";
                case ItemType.CulturePaper:
                    return "Culture Paper";
                case ItemType.Seed:
                    return "Seed";
                case ItemType.Fuel:
                    return "Fuel";
                case ItemType.Ice:
                    return "Ice";
            }
            return string.Empty;
        }
    }
}
