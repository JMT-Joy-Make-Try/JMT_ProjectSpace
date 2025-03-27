using JMT.Building;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{

    public class InventoryUI : PanelUI
    {
        [SerializeField] private Transform content;
        private List<ItemCellUI> cells = new();
        private Button totalButton, itemButton, toolButton, costumeButton;

        private void Awake()
        {
            cells = content.GetComponentsInChildren<ItemCellUI>().ToList();
            Transform buttonGroup = PanelTrm.Find("Panel").Find("Left").Find("ButtonGroup");

            totalButton = buttonGroup.Find("TotalBtn").GetComponent<Button>();
            itemButton = buttonGroup.Find("ItemBtn").GetComponent<Button>();
            toolButton = buttonGroup.Find("ToolBtn").GetComponent<Button>();
            costumeButton = buttonGroup.Find("CostumeBtn").GetComponent<Button>();

            totalButton.onClick.AddListener(() => SelectCategory());
            itemButton.onClick.AddListener(() => SelectCategory(InventoryCategory.Item));
            toolButton.onClick.AddListener(() => SelectCategory(InventoryCategory.Tool));
            costumeButton.onClick.AddListener(() => SelectCategory(InventoryCategory.Costume));
        }
        public override void OpenUI()
        {
            SelectCategory();
            base.OpenUI();
        }

        private void SelectCategory(InventoryCategory? category = null)
        {
            var dic = InventoryManager.Instance.ItemDictionary;
            var pairs = dic.ToList();
            int falseValue = 0;

            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].SetItemCell(string.Empty, 0);
                if (i < dic.Count)
                {
                    KeyValuePair<ItemSO, int> pair = pairs[i];
                    if (category == null || category == pair.Key.Category)
                        cells[i - falseValue].SetItemCell(pair.Key.ItemName, pair.Value);
                    else falseValue++;
                }
            }
        }
    }
}
