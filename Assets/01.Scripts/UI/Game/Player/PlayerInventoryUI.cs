using DG.Tweening;
using JMT.Item;
using JMT.PlayerCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        [SerializeField] private ItemValueUI itemValueUI;
        [SerializeField] private PlayerInventory playerInventory;

        private void Awake()
        {
            playerInventory.OnInventoryEvent += HandleInventoryEvent;
            HandleInventoryEvent(null, 0);
        }

        private void OnDestroy()
        {
            playerInventory.OnInventoryEvent -= HandleInventoryEvent;
        }

        private void HandleInventoryEvent(ItemSO item, int currentVal)
        {
            int maxVal = playerInventory.MaxInventorySize;
            itemValueUI.SetItemCount(item, currentVal, maxVal);
            if(currentVal ==0) itemValueUI.CloseUI();
            else itemValueUI.OpenUI();
        }
    }
}
