using JMT.Agent;
using JMT.Item;
using JMT.Planets.Tile.Items;
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
            var itemSO = currentItem.Key;
            if (itemSO?.IsUsable == false) return;

            if (itemSO.ItemType is ItemType.OxygenTank or ItemType.PurificationContainer)
            {
                // 산소 공급(값은 바꿔줘야함)
                AgentManager.Instance.Player.HealthCompo.AddOxygen(10);
            }
            else if (itemSO.ItemType is ItemType.LiquidFuel or ItemType.RefinedFuel)
            {
                // 연료 공급(값은 바꿔줘야함)
                GameUIManager.Instance.ResourceCompo.AddFuel(10);
            }
            
            // 아이템 창고에서 빼고 currentItem 초기화
        }

        private void HandleItemOutEvent()
        {
            var itemSO = currentItem.Key;
            if (itemSO?.IsTakeable == false) return;

            AgentManager.Instance.Player.InventoryCompo.AddItem(itemSO, currentItem.Value);

            // 아이템 창고에서 빼고 currentItem 초기화
        }

        private void HandleItemEquipEvent(bool isEquip)
        {
            // isEquip은 무시
            // currentItem을 이용하여 아이템 장착
            // 아이템 장착 해제는 주석으로 표시해주세요.
            
            var itemSO = currentItem.Key;
            if (itemSO is not ToolSO toolSO) return;
            
            toolSO.Equip();
            
            // 만약 해제할거면 toolSO.UnEquip()을 호출하세요.
        }
    }
}
