using JMT.Item;
using JMT.PlayerCharacter;
using JMT.UISystem.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.Station
{
    public class StationStorageController : MonoBehaviour, IOpenablePanel
    {
        [Header("Storage Settings")]
        [SerializeField] private Player player;
        [SerializeField] private StorageSO storageSO;
        [SerializeField] private InventorySO inventorySO;
        [SerializeField] private StationStorageView storageView;
        [SerializeField] private StationSelectView selectView;

        private InventoryModel model;
        private KeyValuePair<ItemSO, int> currentItem;

        private void Awake()
        {
            model = new InventoryModel(inventorySO);

            storageView.SetStorage(storageSO);
            storageView.OnItemSelectEvent += HandleItemSelectEvent;
            storageView.OnCategoryEvent += HandleItemCategoryEvent;

            selectView.OnItemUseEvent += HandleItemUseEvent;
            selectView.OnItemOutEvent += HandleItemOutEvent;
            selectView.OnItemEquipEvent += HandleItemEquipEvent;
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
            currentItem = item;
            selectView.OpenUI();
            selectView.SetSelectPanel(item, model.CalculateItemMaxSize(player, item.Value));
        }

        private void HandleItemUseEvent()
        {
            // currentItem을 이용하여 아이템 사용
        }

        private void HandleItemOutEvent()
        {
            // currentItem을 이용하여 아이템 꺼내기
        }

        private void HandleItemEquipEvent(bool isEquip)
        {
            // isEquip은 무시
            // currentItem을 이용하여 아이템 장착
            // 아이템 장착 해제는 주석으로 표시해주세요.
        }
    }
}
