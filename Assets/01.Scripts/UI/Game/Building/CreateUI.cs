using JMT.Building;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class CreateUI : PanelUI
    {
        private List<CellUI> itemCells;
        private Button createButton;
        private CreateItemSO currentItemSO;
        private ItemBuilding workBuilding;
        private void Awake()
        {
            itemCells = PanelTrm.Find("Left").GetComponentsInChildren<CellUI>().ToList();
            createButton = PanelTrm.Find("Right").Find("CreateBtn").GetComponent<Button>();
            createButton.onClick.AddListener(HandleCreateButton);
        }

        public override void OpenUI()
        {
            base.OpenUI();

            Debug.Log(TileManager.Instance.CurrentTile.CurrentBuilding);
            workBuilding = TileManager.Instance.CurrentTile.CurrentBuilding as ItemBuilding;
            if (workBuilding == null)
            {
                Debug.LogError("작업대가 아닙니다.");
                return;
            }
            var ItemList = workBuilding.data.CreateItemList;

            if (workBuilding != null)
                SetItemList(ItemList);

            int index = 0;
            foreach (var item in ItemList)
            {
                int capturedIndex = index;

                itemCells[capturedIndex].GetComponent<Button>().onClick.AddListener(() =>
                {
                    currentItemSO = item;
                    CreateItemUI.Instance.SetCreatePanel(item);
                });

                index++;
            }
        }

        public void SetItemList(SizeLimitQueue<CreateItemSO> createItemList)
        {
            int index = 0;
            foreach (var item in createItemList)
            {
                if (index >= itemCells.Count) break;

                InventoryManager.Instance.ItemDictionary.TryGetValue(item.ResultItem, out int value);
                itemCells[index].SetCell(item.ResultItem, value.ToString());

                index++;
            }
        }

        private void HandleCreateButton()
        {
            if (currentItemSO.UseFuelCount > GameUIManager.Instance.ResourceCompo.CurrentFuelValue) return;
            if (workBuilding.data.Works.IsFull()) return;

            GameUIManager.Instance.ResourceCompo.AddFuel(-0.1f);
            Debug.Log("작업을 시작합니다~!~! 대기열 리스트에 넣었습니당");
            BuildingWork work = new(currentItemSO.ResultItem.ItemType, currentItemSO.CreateTime);
            workBuilding.data.AddWork(work);
            //InventoryManager.Instance.AddItem(currentItemSO.ResultItem, 1);
        }
    }
}
