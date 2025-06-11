using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.Factory
{
    public class FactoryController : BuildingController
    {
        [SerializeField] private FactoryView view;
        [SerializeField] private FactoryItemView itemView;
        [SerializeField] private FactoryToolView toolView;
        [SerializeField] private FactorySelectView selectView;

        [SerializeField] private List<CreateItemSO> itemList;
        [SerializeField] private List<CreateItemSO> toolList;

        private void Awake()
        {
            view.OnExitButtonEvent += ClosePanel;
            view.OnItemButtonEvent += HandleItemButton;
            view.OnToolButtonEvent += HandleToolButton;

            itemView.OnCreateItemEvent += HandleCreateItemEvent;
            itemView.OnOpenSideViewEvent += HandleItemSelectEvent;

            toolView.OnCreateItemEvent += HandleCreateItemEvent;
            toolView.OnOpenSideViewEvent += HandleToolSelectEvent;
        }

        private void HandleCreateItemEvent(CreateItemSO item)
        {
            Debug.Log(item + " 아이템 생성");
        }

        private void HandleItemSelectEvent()
        {
            selectView.SetCells(itemList);
        }

        private void HandleToolSelectEvent()
        {
            selectView.SetCells(toolList);
        }

        public override void OpenPanel()
        {
            view.OpenUI();
            GameUIManager.Instance.GameUICompo.CloseUI();
            GameUIManager.Instance.PlayerControlActive(false);
            SetCurrentPanel(itemView);
        }

        public override void ClosePanel()
        {
            view.CloseUI();
            GameUIManager.Instance.GameUICompo.OpenUI();
            GameUIManager.Instance.PlayerControlActive(true);
            SetCurrentPanel(null);
        }

        private void HandleItemButton()
        {
            SetCurrentPanel(itemView);
        }

        private void HandleToolButton()
        {
            SetCurrentPanel(toolView);
        }
    }
}
