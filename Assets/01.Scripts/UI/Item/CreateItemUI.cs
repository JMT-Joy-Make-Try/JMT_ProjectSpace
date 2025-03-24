using JMT.Core.Tool;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class CreateItemUI : MonoSingleton<CreateItemUI>
    {
        private List<ItemCellUI> cells = new();
        private TextMeshProUGUI resultItemText, useFuelText;
        private Image resultItemIcon;

        protected override void Awake()
        {
            cells = transform.Find("NeedItemList").GetComponentsInChildren<ItemCellUI>().ToList();
            resultItemText = transform.Find("ResultItem").GetComponentInChildren<TextMeshProUGUI>();
            useFuelText = transform.Find("UseFuelTxt").GetComponent<TextMeshProUGUI>();
        }

        public void SetCreatePanel(CreateItemSO itemSO)
        {
            for(int i = 0; i < cells.Count; i++)
            {
                if(i <itemSO.NeedItemList.Count)
                {
                    var pairs = itemSO.NeedItemList.ToList();
                    KeyValuePair<ItemSO, int> pair = pairs[i];
                    cells[i].SetItemCell(pair.Key.ItemName, pair.Value);
                }
                else
                    cells[i].SetItemCell("LOCK", 0);
            }
            resultItemText.text = ObjectExtension.GetName(itemSO.ResultItem);
            useFuelText.text = itemSO.UseFuelCount + " 연료 소모";
        }
    }
}
