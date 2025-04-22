using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Item;
using UnityEngine;

namespace JMT.UISystem.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryView view;
        private readonly InventoryModel model = new();
        
        private ItemSO _curItemSO;
        private void Awake()
        {
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

        public void OpenUI() => view.OpenUI();

        public void CloseUI() => view.CloseUI();

        public void AddItem(ItemSO itemSO, int increaseValue)
        {
            model.AddItem(itemSO, increaseValue);
            GameUIManager.Instance.ItemGetCompo.GetItem(itemSO, increaseValue);
        }

        public void CalculateItem(SerializedDictionary<ItemSO, int> needItems)
        {
            bool isCalculate = model.CalculateItem(needItems);
            if(!isCalculate)
            {
                GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("자원이 부족합니다.");
            }
        }

        private void SelectCategory(InventoryCategory? category)
        {
            var list = model.SelectCategory(category);
            view.ChangeCell(list);
        }

    }
}
