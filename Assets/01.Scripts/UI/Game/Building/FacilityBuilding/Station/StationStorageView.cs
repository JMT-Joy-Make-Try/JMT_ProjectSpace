using JMT.Item;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Station
{
    public class StationStorageView : PanelUI
    {
        public event Action<InventoryCategory?> OnCategoryEvent;
        public event Action<KeyValuePair<ItemSO, int>> OnItemSelectEvent;
        private List<Action> cellHandlers = new();

        [SerializeField] private Button itemCategoryButton, toolCategoryButton; 
        [SerializeField] private Transform cellContent;

        private List<CellUI> cells = new();

        private void Awake()
        {
            itemCategoryButton.onClick.AddListener(HandleItemCategory);
            toolCategoryButton.onClick.AddListener(HandleToolCategory);
        }

        private void HandleItemCategory()
        {
            OnCategoryEvent?.Invoke(InventoryCategory.Item);
        }

        private void HandleToolCategory()
        {
            OnCategoryEvent?.Invoke(InventoryCategory.Tool);
        }

        public void SetStorage(StorageSO storage)
        {
            for (int i = 0; i < storage.TotalCellCount; ++i)
            {
                CellUI cell = Instantiate(storage.ItemCellUI, cellContent);
                cells.Add(cell);
            }
            ResetData();
        }

        public void SetData(List<KeyValuePair<ItemSO, int>> datas)
        {
            if (cells.Count < datas.Count)
            {
                Debug.LogError("기술 상의 문제가 있습니다. 기지 시스템을 확인해 주세요.");
                return;
            }
            ResetData();
            for (int i = 0; i < cells.Count; ++i)
            {
                if (i < datas.Count)
                {
                    int value = i;
                    cellHandlers.Add(() => OnItemSelectEvent?.Invoke(datas[value]));
                    cells[i].SetCell(datas[i].Key, $"X{datas[i].Value}");
                    cells[i].OnClickCellEvent += cellHandlers[i];
                }
            }
        }

        public void ResetData()
        {
            for (int i = 0; i < cells.Count; ++i)
            {
                if (i < cellHandlers.Count)
                {
                    Debug.Log("칸 데이터 초기화");
                    cells[i].OnClickCellEvent -= cellHandlers[i];
                }
                cells[i].ResetCell();
            }
            cellHandlers.Clear();
        }
    }
}
