using JMT.UISystem;
using System;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class BuySpaceshipQuest : QuestBase
    {
        public override void Enable()
        {
            base.Enable();
            GameUIManager.Instance.TradeCompo.OnAcceptEvent += HandleAcceptEvent;
        }

        private void OnDestroy()
        {
            GameUIManager.Instance.TradeCompo.OnAcceptEvent -= HandleAcceptEvent;
        }

        private void HandleAcceptEvent()
        {
            RunQuest(0);
        }
    }
}
