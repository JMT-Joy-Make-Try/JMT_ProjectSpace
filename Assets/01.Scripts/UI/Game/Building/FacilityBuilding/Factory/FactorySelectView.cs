using JMT.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.UISystem.Factory
{
    public class FactorySelectView : SidePanelUI
    {
        public event Action<CreateItemSO> OnSelectItemEvent;
        [SerializeField] private Transform cellContent;

        private List<CellUI> cells = new();
        private List<Action> handlers = new();

        private void Awake()
        {
            cells = cellContent.GetComponentsInChildren<CellUI>().ToList();
        }

        private void OnDestroy()
        {
            ResetCells();
        }

        public void SetCells(List<CreateItemSO> items)
        {
            ResetCells();
            for (int i = 0; i < cells.Count; i++)
            {
                if (i < items.Count)
                {
                    int value = i;
                    handlers.Add(() => OnSelectItemEvent?.Invoke(items[value]));
                    cells[i].SetCell(items[i].ResultItem);
                    cells[i].OnClickCellEvent += handlers[i];
                }
                else
                    cells[i].ResetCell();
            }
        }

        public void ResetCells()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].ResetCell();
                if(i < handlers.Count)
                    cells[i].OnClickCellEvent -= handlers[i];
            }
            handlers.Clear();
        }
    }
}
