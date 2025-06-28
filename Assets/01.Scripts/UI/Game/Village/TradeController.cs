using JMT.Agent.Trader;
using System;
using UnityEngine;

namespace JMT.UISystem.Village
{
    public class TradeController : MonoBehaviour
    {
        [SerializeField] private TradeView view;
        private ITradeable tradeable;

        public event Action OnAcceptEvent
        {
            add => view.OnAcceptEvent += value;
            remove => view.OnAcceptEvent -= value;
        }

        private void Awake()
        {
            view.OnAcceptEvent += HandleAcceptEvent;
            view.OnExitEvent += ClosePanel;
        }

        private void OnDestroy()
        {
            view.OnAcceptEvent -= HandleAcceptEvent;
            view.OnExitEvent -= ClosePanel;
        }

        public void OpenPanel(ITradeable trade)
        {
            tradeable = trade;
            view.SetTradePanel(trade);
            Debug.Log("Trade : " + trade);
            view.OpenUI();
        }

        public void ClosePanel()
        {
            tradeable = null;
            view.CloseUI();
        }

        private void HandleAcceptEvent()
        {
            if (tradeable == null) return;

            Debug.Log("퀘스트 연결이 필요합니다.");
            tradeable.SucessTrade();
            view.CloseUI();
        }
    }
}
