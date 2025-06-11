using DG.Tweening;
using JMT.UISystem;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace JMT
{
    public class FactoryItemView : PanelUI
    {
        public event Action OnOpenSideViewEvent;
        public event Action<CreateItemSO> OnCreateItemEvent;

        [SerializeField] private CellUI createItem;
        [SerializeField] private CanvasGroup beforePanel, afterPanel;
        [SerializeField] private TextMeshProUGUI itemNameText, itemDescText;
        [SerializeField] private List<CellUI> needItemList = new();
        [SerializeField] private Button createButton;

        private Button createItemButton;
        private CreateItemSO currentItem;

        private void Awake()
        {
            createItemButton = createItem.GetComponent<Button>();

            createItemButton.onClick.AddListener(HandleCreateItemButton);
            createButton.onClick.AddListener(HandleCreateButton);
        }

        private void OnDestroy()
        {
            createButton.onClick.RemoveListener(HandleCreateButton);
        }

        private void HandleCreateItemButton()
        {
            OnOpenSideViewEvent?.Invoke();
        }

        private void HandleCreateButton()
        {
            if (currentItem != null)
                OnCreateItemEvent?.Invoke(currentItem);
        }

        public void SetSelectItemPanel(CreateItemSO item)
        {
            beforePanel.DOFade(0, 0.3f);
            afterPanel.DOFade(1, 0.3f);

            currentItem = item;
            createItem.SetCell(item.ResultItem);

            itemNameText.text = item.ResultItem.ItemName;
            itemDescText.text = item.ResultItem.ItemDescription;

            var needItems = item.NeedItemList.ToList();
            for (int i = 0; i < needItemList.Count; i++)
            {
                if (i < needItems.Count)
                    needItemList[i].SetCell(needItems[i].Key, needItems[i].Value.ToString());
            }
        }

        public override void CloseUI()
        {
            base.CloseUI();

            beforePanel.DOFade(1, 0.3f);
            afterPanel.DOFade(0, 0.3f);

            if (currentItem != null)
                currentItem = null;
        }
    }
}
