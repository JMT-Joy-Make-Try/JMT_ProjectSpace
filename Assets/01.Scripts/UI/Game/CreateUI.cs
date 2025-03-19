using AYellowpaper.SerializedCollections;
using JMT.Building;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using System.Linq;

namespace JMT.UISystem
{
    public class CreateUI : PanelUI
    {
        private List<ItemCellUI> itemCells;

        private void Awake()
        {
            itemCells = PanelTrm.Find("Panel").Find("Left").GetComponentsInChildren<ItemCellUI>().ToList();
        }

        public override void OpenUI()
        {
            WorkingBuilding workBuilding = TileManager.Instance.CurrentTile.CurrentBuilding as WorkingBuilding;

            if(workBuilding != null) 
                SetItemList(workBuilding.CreateItemList);

            base.OpenUI();
        }

        public void SetItemList(SerializedDictionary<ItemType, bool> createItemList)
        {
            for (int i = 0; i < itemCells.Count; ++i)
            {
                if (createItemList.Count <= i) return;

                var pairs = createItemList.ToList();
                KeyValuePair<ItemType, bool> pair = pairs[i];

                InventoryManager.Instance.ItemDictionary.TryGetValue(pair.Key, out int value);
                itemCells[i].SetItemCell(ObjectExtension.GetName(pair.Key), value);
            }
        }
    }
}
