using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Item;
using JMT.Planets.Tile.Items;
using Unity.Properties;
using UnityEngine;

namespace JMT.UISystem.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryView view;
        [SerializeField] private InventorySO inventorySO;
        private InventoryModel model;

        private ItemSO _curItemSO;
        public InventorySO InventorySO => inventorySO;
        
        private void Awake()
        {
            inventorySO = Instantiate(inventorySO);
            model = new InventoryModel(inventorySO);
            view.OnCategoryChangedEvent += SelectCategory;
            view.OnItemAddedEvent += HandleItemAdded;
            view.OnEquipButtonClickedEvent += HandleEquip;
        }

        private void HandleEquip()
        {
            Player.Player player = AgentManager.Instance.Player;
            //player.PlayerTool.SetCloth();
        }

        private void HandleItemAdded(ItemSO so)
        {
            view.HandleCellButton(so);
            _curItemSO = so;
        }

        private void OnDestroy()
        {
            view.OnCategoryChangedEvent -= SelectCategory;
            view.OnItemAddedEvent -= HandleItemAdded;
        }

        public void OpenUI()
        {
            view.OpenUI();
            SelectCategory(null);
        }

        public void CloseUI() => view.CloseUI();

        public void AddItem(ItemSO itemSO, int increaseValue)
        {
            Debug.Log("네 아이템 들어왔어요.");
            model.AddItem(itemSO, increaseValue);
            GameUIManager.Instance.ItemGetCompo.GetItem(itemSO, increaseValue);
        }

        public void AddItem(ItemType itemType, int increaseValue)
        {
            ItemSO so = ItemListSystem.Instance.GetItemSO(itemType);
            Debug.Log("네 아이템 들어왔어요.");
            model.AddItem(so, increaseValue);
            GameUIManager.Instance.ItemGetCompo.GetItem(so, increaseValue);
        }

        public bool CalculateItem(SerializedDictionary<ItemSO, int> needItems)
        {
            bool isCalculate = model.CalculateItem(needItems);
            if(!isCalculate)
            {
                GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("자원이 부족합니다.");
            }
            return isCalculate;
        }

        public void RemoveItem(ItemSO item, int value) => model.RemoveItem(item, value);

        private void SelectCategory(InventoryCategory? category)
        {
            var list = model.SelectCategory(category);
            view.ChangeCell(list);
        }
    }
}
