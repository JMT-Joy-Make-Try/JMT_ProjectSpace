using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.SupplyOxygen
{
    public class OxygenWorkView : PanelUI
    {
        public event Action OnOpenSideViewEvent;
        public event Action<CreateItemSO> OnCreateItemEvent;

        [SerializeField] private CellUI createItem;
        [SerializeField] private CanvasGroup beforePanel, afterPanel;
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
            if(currentItem != null)
            OnCreateItemEvent?.Invoke(currentItem);
        }

        public void SetCreateItemButton()
        {

        }

        public void SetSelectItemPanel(CreateItemSO item)
        {
            beforePanel.DOFade(0, 0.3f);
            afterPanel.DOFade(1, 0.3f);

            currentItem = item;
            createItem.SetCell(item.ResultItem);
            var needItems = item.NeedItemList.ToList();
            for(int i = 0; i < needItemList.Count; i++)
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

            if(currentItem!= null)
                currentItem = null;
        }
    }
}
