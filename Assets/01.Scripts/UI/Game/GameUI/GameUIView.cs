using JMT.Item;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT
{
    public class GameUIView : PanelUI
    {
        public event Action<ToolSO, bool> OnSelectToolEvent;
        [SerializeField] private Transform toolContent;
        private List<CellUI> toolCells = new();
        private List<Action> handlers = new();
        private int currentToolIndex;

        private void Awake()
        {
            toolCells = toolContent.GetComponentsInChildren<CellUI>().ToList();
            Debug.Log(toolCells.Count);
        }

        public void SetTools(List<ToolSO> tools)
        {
            ResetTools();
            for (int i = 0; i < toolCells.Count; i++)
            {
                if (i < tools.Count)
                {
                    int value = i;
                    handlers.Add(() => OnSelectToolEvent?.Invoke(tools[value], ChangeSelect(value)));
                    toolCells[i].SetCell(tools[i]);
                    toolCells[i].OnClickCellEvent += handlers[i];
                }
            }
        }

        public void ResetTools()
        {
            for (int i = 0; i < toolCells.Count; i++)
            {
                if (i < handlers.Count)
                    toolCells[i].OnClickCellEvent -= handlers[i];
                toolCells[i].ResetCell();
            }
            handlers.Clear();
        }

        public bool ChangeSelect(int value)
        {
            toolCells[currentToolIndex].SetSelect(false);
            currentToolIndex = value;
            return toolCells[value].ChangeSelect();
        }
    }
}
