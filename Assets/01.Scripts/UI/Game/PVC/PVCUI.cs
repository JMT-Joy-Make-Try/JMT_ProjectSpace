using JMT.Planets.Tile;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem
{
    public class PVCUI : MonoBehaviour
    {
        [SerializeField] private GameObject fill;
        [SerializeField] private List<ItemValueUI> needItemValue;

        private void Awake()
        {
            ActiveFillUI(false);
        }

        public void ActiveFillUI(bool isFillActive)
            => fill.SetActive(isFillActive);

        public void ActiveItemUI(bool isItemActive)
        {
            foreach (var item in needItemValue)
            {
                if (isItemActive) item.OpenUI();
                else item.CloseUI();
            }
        }

        public void SetNeedItemUI(List<PreBuildItemData> items)
        {
            for(int i = 0; i  < needItemValue.Count; i++)
            {
                if (i < items.Count)
                {
                    needItemValue[i].OpenUI();
                    needItemValue[i].SetItemCount(items[i].Item, items[i].CurItemCount, items[i].MaxItemCount);
                }
                else
                    needItemValue[i].CloseUI();
            }
        }
    }
}
