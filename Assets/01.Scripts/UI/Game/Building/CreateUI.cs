using AYellowpaper.SerializedCollections;
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
        private List<ItemCellUI> itemCells;

        /*private void Awake()
        {
            itemCells = PanelTrm.Find("Panel").Find("Left").GetComponentsInChildren<ItemCellUI>().ToList();
        }

        public override void OpenUI()
        {
            base.OpenUI();

            WorkingBuilding workBuilding = TileManager.Instance.CurrentTile.CurrentBuilding as WorkingBuilding;
            var ItemList = workBuilding.CreateItemList;

            if (workBuilding != null)
                SetItemList(ItemList);

            var pairs = ItemList.ToList();
            Debug.Log(pairs.Count);

            for (int i = 0; i < pairs.Count; i++)
            {
                Debug.Log("ㅁㄴㅇㄹ");
                int value = i;
                itemCells[value].GetComponent<Button>().onClick.AddListener(() =>
                {
                    Debug.Log("itemCells[i]" + itemCells[value] + "/i" + value );
                    CreateItemUI.Instance.SetCreatePanel(pairs[value].Key);
                });
            }
        }*/

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
    }
}
