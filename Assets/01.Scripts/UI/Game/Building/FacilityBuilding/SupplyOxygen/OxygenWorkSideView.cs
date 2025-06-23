using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.SupplyOxygen
{
    public class OxygenWorkSideView : SidePanelUI
    {
        public event Action<CreateItemSO> OnSelectItemEvent;
        [SerializeField] private List<CreateItemSO> createItemList;
        [SerializeField] private List<CellUI> itemUI;

        private List<Action> handlers = new();

        private void Awake()
        {
            for (int i = 0; i < itemUI.Count; i++)
            {
                int value = i;

                if (value < createItemList.Count)
                {
                    handlers.Add(() => HandleItemUIButton(value));
                    itemUI[value].SetCell(createItemList[value].ResultItem);
                    itemUI[value].OnClickCellEvent += handlers[i];
                }
                else itemUI[value].ResetCell();
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < handlers.Count; ++i)
            {
                itemUI[i].OnClickCellEvent -= handlers[i];
            }
        }

        private void HandleItemUIButton(int value)
        {
            OnSelectItemEvent?.Invoke(createItemList[value]);
        }
    }
}
