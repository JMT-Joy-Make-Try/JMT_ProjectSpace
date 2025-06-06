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

        public void SetSelectPanel(KeyValuePair<ItemSO, int> item, int maxValue)
        {
            itemIcon.sprite = item.Key.DisplayIcon;
            itemNameText.text = item.Key.DisplayName;
            itemDescText.text = item.Key.ItemDescription;

            itemCountUI.Init(maxValue);
            useButton.onClick.AddListener(HandleUseButton);
            outButton.onClick.AddListener(HandleOutButton);
            equipButton.onClick.AddListener(HandleEquipButton);
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
            // 장비 장착 여부를 어떻게 판단하죠
            OnItemEquipEvent?.Invoke(false);
        }
    }
}
