using JMT.Item;
using JMT.UISystem.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.Station
{
    public class StationStorageController : MonoBehaviour, IOpenablePanel
    {
        [Header("Storage Settings")]
        [SerializeField] private StorageSO storageSO;
        [SerializeField] private InventorySO inventorySO;
        [SerializeField] private StationStorageView storageView;
        [SerializeField] private StationSelectView selectView;

        private InventoryModel model;

        private void Awake()
        {
            model = new InventoryModel(inventorySO);

            storageView.SetStorage(storageSO);
            storageView.OnItemSelectEvent += HandleItemSelectEvent;
            storageView.OnCategoryEvent += HandleItemCategoryEvent;
        }

        public void OpenUI()
        {
            storageView.OpenUI();
            HandleItemCategoryEvent(InventoryCategory.Item);
        }

        public void CloseUI()
        {
            storageView.CloseUI();
            selectView.CloseUI();
        }

        private void HandleItemCategoryEvent(InventoryCategory? category)
        {
            var list = model.SelectCategory(category);
            storageView.SetData(list);
        }

        private void HandleItemSelectEvent(KeyValuePair<ItemSO, int> item)
        {
            selectView.OpenUI();
            selectView.SetSelectPanel(item);
        }
    }
}
