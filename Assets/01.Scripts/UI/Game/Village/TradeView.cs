using JMT.Agent.Trader;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Village
{
    public class TradeView : PanelUI
    {
        public event Action OnAcceptEvent;
        public event Action OnExitEvent;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descText;
        [SerializeField] private TextMeshProUGUI receiveText;
        [SerializeField] private CellUI receiveCell;
        [SerializeField] private Transform needItemListTrm;
        [SerializeField] private Button acceptButton, exitButton;
        
        private List<CellUI> needItems;

        private void Awake()
        {
            needItems = needItemListTrm.GetComponentsInChildren<CellUI>().ToList();
            acceptButton.onClick.AddListener(HandleAcceptButton);
            exitButton.onClick.AddListener(HandleCloseButton);
        }

        private void OnDestroy()
        {
            acceptButton.onClick.RemoveListener(HandleAcceptButton);
            exitButton.onClick.RemoveListener(HandleCloseButton);
        }

        public void SetTradePanel(ITradeable trade)
        {
            titleText.text = trade.TitleText;
            descText.text = trade.Description;
            receiveText.text = trade.ReceiveText;
            receiveCell.SetCell(null, $"X {trade.ReceiveCount}");

            for(int i = 0; i < needItems.Count; i++)
            {
                var pairs = trade.NeedItems.ToList();
                bool isNeedItem = i < pairs.Count;

                if (isNeedItem)
                    needItems[i].SetCell(pairs[i].Key, $"X {pairs[i].Value}");

                needItems[i].gameObject.SetActive(isNeedItem);
            }
        }

        private void HandleAcceptButton()
        {
            OnAcceptEvent?.Invoke();
        }

        private void HandleCloseButton()
        {
            OnExitEvent?.Invoke();
        }
    }
}
