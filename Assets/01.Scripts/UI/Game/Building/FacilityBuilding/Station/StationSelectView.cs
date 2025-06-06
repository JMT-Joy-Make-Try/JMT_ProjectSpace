using JMT.Item;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Station
{
    public class StationSelectView : SidePanelUI
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemNameText, itemDescText;
        [SerializeField] private ItemCountUI itemCountUI;
        [SerializeField] private Button useButton, outButton;


        public void SetSelectPanel(KeyValuePair<ItemSO, int> item)
        {
            itemIcon.sprite = item.Key.DisplayIcon;
            itemNameText.text = item.Key.DisplayName;
            itemDescText.text = item.Key.ItemDescription;

            itemCountUI.Init(item.Value);
            useButton.onClick.AddListener(HandleUseButton);
            outButton.onClick.AddListener(HandleOutButton);
        }

        private void HandleUseButton()
        {
            Debug.Log("아이템을 사용합니다.");
            //itemCountUI.Count
        }

        private void HandleOutButton()
        {
            Debug.Log("아이템을 꺼냈습니다.");
            //itemCountUI.Count
        }
    }
}
