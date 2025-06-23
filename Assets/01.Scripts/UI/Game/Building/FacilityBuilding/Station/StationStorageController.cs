using JMT.Agent;
using JMT.Item;
using JMT.Planets.Tile.Items;
using JMT.PlayerCharacter;
using JMT.UISystem.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.Station
{
    // selectView.ItemCountUI.Count 쓰는곳 고쳐주세요
    public class StationStorageController : MonoBehaviour, IOpenablePanel
    {
        public event Action OnEndEvent;

        [Header("Storage Settings")]
        [SerializeField] private Player player;
        [SerializeField] private StorageSettingsSO storageSO;
        [SerializeField] private InventorySO inventorySO;
        [SerializeField] private StationStorageView storageView;
        [SerializeField] private StationSelectView selectView;

        private StationStorageModel model;
        private KeyValuePair<ItemSO, int> currentItem;

        private void Awake()
        {
            model = new StationStorageModel(inventorySO);

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


        // 아이템 카테고리 선택 이벤트
        private void HandleItemCategoryEvent(InventoryCategory? category)
        {
            var list = model.SelectCategory(category);
            storageView.SetData(list);
        }

        // 아이템 셀 선택 이벤트
        private void HandleItemSelectEvent(KeyValuePair<ItemSO, int> item)
        {
            int itemMaxValue = model.CalculateItemMaxSize(player, item.Value);
            currentItem = new(item.Key, itemMaxValue);
            selectView.OpenUI();
            selectView.SetSelectPanel(item, itemMaxValue);
        }

        // 아이템 사용 이벤트
        private void HandleItemUseEvent()
        {
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
            Debug.Log(selectView.ItemCountUI.Count + "개 아이템 꺼내기");
            model.RemoveItem(currentItem.Key, selectView.ItemCountUI.Count);
            OnEndEvent?.Invoke();
        }

        // 아이템 꺼내기 이벤트
        private void HandleItemOutEvent()
        {
            var itemSO = currentItem.Key;
            if (itemSO?.IsTakeable == false) return;

            Debug.Log(selectView.ItemCountUI.Count + "개 아이템 꺼내기");
            AgentManager.Instance.Player.InventoryCompo.AddItem(itemSO, selectView.ItemCountUI.Count);

            // 아이템 창고에서 빼고 currentItem 초기화
            model.RemoveItem(currentItem.Key, selectView.ItemCountUI.Count);
            OnEndEvent?.Invoke();
        }

        // 아이템 장착 이벤트
        private void HandleItemEquipEvent(bool isEquip)
        {
            // isEquip은 무시
            // currentItem을 이용하여 아이템 장착
            // 아이템 장착 해제는 주석으로 표시해주세요.
            
            var itemSO = currentItem.Key;
            if (itemSO is not ToolSO toolSO) return;
            
            player.PlayerToolCompo.AddTool(toolSO);
            toolSO.Equip();
            
            // 만약 해제할거면 toolSO.UnEquip()을 호출하세요.
        }

        public void AddItem(ItemSO item, int amount)
        {
            model.AddItem(item, amount);
        }

        public bool HasItem(ItemSO item, int amount)
            => model.HasItem(item, amount);

        public int FindItem(CreateItemSO item)
            => model.FindItem(item.ResultItem);

        public void RemoveItem(ItemSO key, int value)
            => model.RemoveItem(key, value);
    }
}
