using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.SupplyOxygen
{
    public class OxygenWorkSideView : SidePanelUI
    {
        public event Action<CreateItemSO> OnSelectItemEvent;
        [SerializeField] private List<CreateItemSO> createItemList;
        [SerializeField] private List<CellUI> itemUI;

        private void Awake()
        {
            for(int i = 0; i < itemUI.Count; i++)
            {
                int value = i;

                if (value < createItemList.Count)
                {
                    itemUI[value].SetCell(createItemList[value].ResultItem);
                    itemUI[value].GetComponent<Button>().onClick.AddListener(() => HandleItemUIButton(value));
                }
                else itemUI[value].ResetCell();
            }
        }

        private void HandleItemUIButton(int value)
        {
            OnSelectItemEvent?.Invoke(createItemList[value]);
        }
    }
}
