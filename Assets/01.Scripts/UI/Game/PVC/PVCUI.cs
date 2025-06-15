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
            ActiveUI(false, true);
        }

        public void ActiveUI(bool isFillActive, bool isNeedItemActive)
        {
            fill.SetActive(isFillActive);
            foreach (var item in needItemValue)
                item.CloseUI();
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
