using JMT.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Inventory
{
    public class InventoryView : PanelUI
    {
        public event Action<InventoryCategory?> OnCategoryChangedEvent;

        [SerializeField] private Button inventoryButton;
        [SerializeField] private Transform cellContents;

        [Header("Category Button")]
        [SerializeField] private Button totalButton;
        [SerializeField] private Button itemButton;
        [SerializeField] private Button toolButton;
        [SerializeField] private Button costumeButton;

        [Header("Select Info")]
        [SerializeField] private Image infoIcon;
        [SerializeField] private TextMeshProUGUI infoNameText, infoDescText, infoLocationText;

        [Header("Info Button Group")]
        [SerializeField] private GameObject infoButtonGroup;
        [SerializeField] private Button clearButton, EquipButton;

        private List<CellUI> cells = new();

        private void Awake()
        {
            cells = cellContents.GetComponentsInChildren<CellUI>().ToList();

            inventoryButton.onClick.AddListener(OpenUI);
            totalButton.onClick.AddListener(() => OnCategoryChangedEvent?.Invoke(null));
            itemButton.onClick.AddListener(() => OnCategoryChangedEvent?.Invoke(InventoryCategory.Item));
            toolButton.onClick.AddListener(() => OnCategoryChangedEvent?.Invoke(InventoryCategory.Tool));
            costumeButton.onClick.AddListener(() => OnCategoryChangedEvent?.Invoke(InventoryCategory.Costume));
        }

        public override void OpenUI()
        {
            ResetCell();
            base.OpenUI();
        }

        public void ChangeCell(List<KeyValuePair<ItemSO, int>> itemList)
        {
            ResetCell();
            for (int i = 0; i < cells.Count; i++)
            {
                int value = i;
                if (value < itemList.Count)
                {
                    cells[value].GetComponent<Button>().onClick.AddListener(() => HandleCellButton(itemList[value].Key));
                    cells[i].SetCell(itemList[value].Key, itemList[value].Value.ToString());
                }
            }
        }

        private void HandleCellButton(ItemSO data)
        {
            if (data.Icon != null)
                infoIcon.sprite = data.Icon;
            infoNameText.text = data.ItemName;
            infoDescText.text = data.ItemDescription;
            infoButtonGroup.SetActive(!data.Category.Equals(InventoryCategory.Item));
        }

        private void ResetCell()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                int value = i;
                cells[value].GetComponent<Button>().onClick.RemoveAllListeners();
                cells[i].ResetCell();
            }
        }
    }
}
