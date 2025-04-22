using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Item;
using Unity.Properties;
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
            player.PlayerTool.SetCloth(((_curItemSO) as ToolSO).ToolType);
            Debug.Log(player.PlayerTool._curPlayerToolSO);
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
            view.OnEquipButtonClickedEvent -= HandleEquip;
        }

        public void OpenUI() => view.OpenUI();

        public void CloseUI() => view.CloseUI();

        public void AddItem(ItemSO itemSO, int increaseValue)
        {
            Debug.Log("네 아이템 들어왔어요.");
            model.AddItem(itemSO, increaseValue);
            GameUIManager.Instance.ItemGetCompo.GetItem(itemSO, increaseValue);
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

        private void SelectCategory(InventoryCategory? category)
        {
            var list = model.SelectCategory(category);
            view.ChangeCell(list);
        }
    }
}
