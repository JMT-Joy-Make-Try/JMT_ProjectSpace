using AYellowpaper.SerializedCollections;
using JMT.Building;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using JMT.Resource;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class CreateUI : PanelUI
    {
        private List<ItemCellUI> itemCells;
        private Button createButton;
        private CreateItemSO currentItemSO;
        private ItemBuilding workBuilding;
        private void Awake()
        {
            itemCells = PanelTrm.Find("Left").GetComponentsInChildren<ItemCellUI>().ToList();
            createButton = PanelTrm.Find("Right").Find("CreateBtn").GetComponent<Button>();
            createButton.onClick.AddListener(HandleCreateButton);
        }

        public override void OpenUI()
        {
            base.OpenUI();

            Debug.Log(TileManager.Instance.CurrentTile.CurrentBuilding);
            workBuilding = TileManager.Instance.CurrentTile.CurrentBuilding as ItemBuilding;
            var ItemList = workBuilding.data.CreateItemList;

            if (workBuilding != null)
                SetItemList(ItemList);

            for (int i = 0; i < ItemList.Count; i++)
            {
                int value = i;
                itemCells[value].GetComponent<Button>().onClick.AddListener(() =>
                {
                    currentItemSO = ItemList[value];
                    CreateItemUI.Instance.SetCreatePanel(ItemList[value]);
                });
            }
        }

        public void SetItemList(List<CreateItemSO> createItemList)
        {
            for (int i = 0; i < itemCells.Count; ++i)
            {
                if (createItemList.Count <= i) return;

                InventoryManager.Instance.ItemDictionary.TryGetValue(createItemList[i].ResultItem, out int value);
                itemCells[i].SetItemCell(createItemList[i].ResultItem.ItemName, value, createItemList[i].ResultItem.Icon);
            }
        }

        private void HandleCreateButton()
        {
            if(currentItemSO.UseFuelCount > ResourceManager.Instance.CurrentFuelValue) return;

            ResourceManager.Instance.AddFuel(-currentItemSO.UseFuelCount);
            Debug.Log("작업을 시작합니다~!~! 대기열 리스트에 넣었습니당");
            BuildingWork work = new(currentItemSO.ResultItem.ItemType, currentItemSO.CreateTime);
            workBuilding.data.AddWork(work);
            //InventoryManager.Instance.AddItem(currentItemSO.ResultItem, 1);
        }
    }
}
