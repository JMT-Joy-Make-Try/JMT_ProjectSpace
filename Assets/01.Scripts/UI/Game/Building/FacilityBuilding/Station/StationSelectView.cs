using JMT.Item;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Station
{
    public class StationSelectView : SidePanelUI
    {
        public event Action OnItemUseEvent;
        public event Action OnItemOutEvent;
        public event Action<bool> OnItemEquipEvent;

        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemNameText, itemDescText;
        [SerializeField] private ItemCountUI itemCountUI;
        [SerializeField] private Button useButton, outButton, equipButton;
        
        // 일단 public으로 하고 장서윤이 바꿔주겠지
        public ItemCountUI ItemCountUI => itemCountUI;

        public void SetSelectPanel(KeyValuePair<ItemSO, int> item, int maxValue)
        {
            itemIcon.sprite = item.Key.DisplayIcon;
            itemNameText.text = item.Key.DisplayName;
            itemDescText.text = item.Key.ItemDescription;

            itemCountUI.Init(maxValue);

            ButtonSettings(item);
        }

        private void ButtonSettings(KeyValuePair<ItemSO, int> item)
        {
            ResetListeners();
            equipButton.gameObject.SetActive(item.Key is ToolSO);
            useButton.gameObject.SetActive(item.Key.IsUsable);
            outButton.gameObject.SetActive(item.Key.IsTakeable);

            if (item.Key is ToolSO tool)
            {
                // 장착 여부 확인해야 함.
                if(!tool.IsEquipped)
                    equipButton.onClick.AddListener(HandleEquipButton);
                else
                    equipButton.onClick.AddListener(HandleUnEquipButton);
                return;
            }
            if(item.Key.IsUsable)
                useButton.onClick.AddListener(HandleUseButton);
            if (item.Key.IsTakeable)
                outButton.onClick.AddListener(HandleOutButton);
        }

        private void ResetListeners()
        {
            useButton.onClick.RemoveAllListeners();
            outButton.onClick.RemoveAllListeners();
            equipButton.onClick.RemoveAllListeners();
        }

        private void HandleUseButton()
        {
            OnItemUseEvent?.Invoke();
        }

        private void HandleOutButton()
        {
            OnItemOutEvent?.Invoke();
        }

        private void HandleEquipButton()
        {
            OnItemEquipEvent?.Invoke(true);
        }

        private void HandleUnEquipButton()
        {
            OnItemEquipEvent?.Invoke(false);
        }
    }
}
