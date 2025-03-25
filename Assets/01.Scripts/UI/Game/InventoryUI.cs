using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JMT.UISystem
{
    public class InventoryUI : PanelUI
    {
        [SerializeField] private Transform content;
        private List<ItemCellUI> cells = new();

        private void Awake()
        {
            cells = content.GetComponentsInChildren<ItemCellUI>().ToList();
        }
        public override void OpenUI()
        {
            var dic = InventoryManager.Instance.ItemDictionary;

            for(int i = 0; i < cells.Count; i++)
            {
                if(i < dic.Count)
                {
                    var pairs = dic.ToList();
                    KeyValuePair<ItemSO, int> pair = pairs[i];
                    cells[i].SetItemCell(pair.Key.ItemName, pair.Value);
                }
                else
                    cells[i].SetItemCell(string.Empty, 0);
            }
            base.OpenUI();
        }

        private string GetName(ItemType key)
        {
            string name = Enum.GetName(key.GetType(), key);
            if (string.IsNullOrEmpty(name)) return string.Empty;

            var result = new StringBuilder();
            result.Append(name[0]);

            for (int i = 1; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]))
                    result.Append(' ');

                result.Append(name[i]);
            }

            return result.ToString();
        }
    }
}
