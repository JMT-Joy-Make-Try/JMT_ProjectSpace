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
            WorkingBuilding workBuilding = TileManager.Instance.CurrentTile.CurrentBuilding as WorkingBuilding;
            var ItemList = workBuilding.CreateItemList;

            if (workBuilding != null)
                SetItemList(ItemList);

            var pairs = ItemList.ToList();
            Debug.Log(pairs.Count);

            for (int i = 0; i < pairs.Count; i++)
            {
                int value = i;
                itemCells[value].GetComponent<Button>().onClick.AddListener(() =>
                {
                    currentItemSO = pairs[value].Key;
                    CreateItemUI.Instance.SetCreatePanel(pairs[value].Key);
                });
            }
        }

        public void SetItemList(SerializedDictionary<CreateItemSO, bool> createItemList)
        {
            for (int i = 0; i < itemCells.Count; ++i)
            {
                if (createItemList.Count <= i) return;

                var pairs = createItemList.ToList();
                KeyValuePair<CreateItemSO, bool> pair = pairs[i];

                InventoryManager.Instance.ItemDictionary.TryGetValue(pair.Key.ResultItem, out int value);
                itemCells[i].SetItemCell(ObjectExtension.GetName(pair.Key.ResultItem), value);
            }
        }

        private void HandleCreateButton()
        {
            if(currentItemSO.UseFuelCount > ResourceManager.Instance.CurrentFuelValue) return;

            ResourceManager.Instance.AddFuel(-currentItemSO.UseFuelCount);
            Debug.Log("작업을 시작합니다~!~! 하지만 귀찮으니 그냥 바로 아이템을 추가할 거에요.");
            InventoryManager.Instance.AddItem(currentItemSO.ResultItem, 1);
        }
    }
}
