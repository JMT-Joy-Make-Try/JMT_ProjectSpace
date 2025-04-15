using JMT.Core.Tool;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class CreateItemUI : MonoSingleton<CreateItemUI>
    {
        private List<CellUI> cells = new();
        private TextMeshProUGUI resultItemText, useFuelText;
        private Image resultItemIcon;

        protected override void Awake()
        {
            cells = transform.Find("NeedItemList").GetComponentsInChildren<CellUI>().ToList();
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
                    var pair = pairs[i];
                    cells[i].SetCell(pair.Key, pair.Value.ToString());
                }
                else
                    cells[i].ResetCell();
            }
            resultItemText.text = itemSO.ResultItem.ItemName;
            useFuelText.text = itemSO.UseFuelCount + " 연료 소모";
        }
    }
}
