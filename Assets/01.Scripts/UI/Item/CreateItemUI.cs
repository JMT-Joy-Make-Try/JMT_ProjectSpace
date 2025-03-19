using JMT.Core.Tool;
using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class CreateItemUI : MonoSingleton<CreateItemUI>
    {
        private List<ItemCellUI> cells = new();
        private TextMeshProUGUI resultItemText;
        private Image resultItemIcon;

        protected override void Awake()
        {
            cells = transform.Find("NeedItemList").GetComponentsInChildren<ItemCellUI>().ToList();
            resultItemText = transform.Find("ResultItem").GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetCreatePanel(CreateItemSO itemSO)
        {
            for(int i = 0; i < cells.Count; i++)
            {
                if(i <itemSO.NeedItemList.Count)
                {
                    var pairs = itemSO.NeedItemList.ToList();
                    KeyValuePair<ItemType, int> pair = pairs[i];
                    cells[i].SetItemCell(ObjectExtension.GetName(pair.Key), pair.Value);
                }
                else
                    cells[i].SetItemCell("LOCK", 0);
            }
            resultItemText.text = ObjectExtension.GetName(itemSO.ResultItem);
        }
    }
}
