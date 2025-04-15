using JMT.Building;
using JMT.Core.Manager;
using JMT.Item;
using JMT.Planets.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{

    public class InventoryUI : PanelUI
    {
        [SerializeField] private Transform content, right;
        private List<CellUI> cells = new();
        private Button totalButton, itemButton, toolButton, costumeButton;

        private Image icon;
        private TextMeshProUGUI nameText, descriptionText, locationText;
        private GameObject buttonGroup;
        private Button clearButton, EquipButton;

        private void Awake()
        {
            cells = content.GetComponentsInChildren<CellUI>().ToList();
            Transform leftGroup = PanelTrm.Find("Panel").Find("Left").Find("ButtonGroup");

            totalButton = leftGroup.Find("TotalBtn").GetComponent<Button>();
            itemButton = leftGroup.Find("ItemBtn").GetComponent<Button>();
            toolButton = leftGroup.Find("ToolBtn").GetComponent<Button>();
            costumeButton = leftGroup.Find("CostumeBtn").GetComponent<Button>();

            totalButton.onClick.AddListener(() => SelectCategory());
            itemButton.onClick.AddListener(() => SelectCategory(InventoryCategory.Item));
            toolButton.onClick.AddListener(() => SelectCategory(InventoryCategory.Tool));
            costumeButton.onClick.AddListener(() => SelectCategory(InventoryCategory.Costume));

            icon = right.Find("Icon").GetComponent<Image>();
            nameText = right.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>();
            descriptionText = right.Find("Description").GetComponentInChildren<TextMeshProUGUI>();
            locationText = right.Find("Location").GetComponentInChildren<TextMeshProUGUI>();
            buttonGroup = right.Find("ButtonGroup").gameObject;
            clearButton = buttonGroup.transform.Find("ClearBtn").GetComponent<Button>();
            EquipButton = buttonGroup.transform.Find("EquipBtn").GetComponent<Button>();
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
                int value = i;
                cells[value].GetComponent<Button>().onClick.RemoveAllListeners();
                cells[i].ResetCell();
                if (i < dic.Count)
                {
                    var pair = pairs[i];
                    if (category == null || category.Value.Equals(pair.Key.Category))
                    {
                        cells[value - falseValue].GetComponent<Button>().onClick.AddListener(()=> HandleCellButton(pair.Key));
                        cells[i - falseValue].SetCell(pair.Key, pair.Value.ToString());
                    }    
                    else falseValue++;
                }
            }
        }

        private void HandleCellButton(ItemSO data)
        {
            if(data.Icon != null)
                icon.sprite = data.Icon;
            nameText.text = data.ItemName;
            descriptionText.text = data.ItemDescription;
            buttonGroup.SetActive(!data.Category.Equals(InventoryCategory.Item));
        }
    }
}
