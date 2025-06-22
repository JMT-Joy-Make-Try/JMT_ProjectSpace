using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Item;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public class TraderTrade : MonoBehaviour, ITraderComponent, IItemReceivable
    {
        public Trader Trader { get; private set; }
        
        [SerializeField] private SerializedDictionary<ItemSO, int> _tradeItems = new();
        
        public void Init(Trader trader)
        {
            Trader = trader;
            Trader.GetTraderComponent<TraderTimer>().OnTimerComplete += HandleTimerComplete;
        }
        
        private void OnDestroy()
        {
            if (Trader != null)
            {
                Trader.GetTraderComponent<TraderTimer>().OnTimerComplete -= HandleTimerComplete;
            }
        }

        private void HandleTimerComplete()
        {
            Trader.StateMachineCompo.ChangeState(TraderStateEnum.Disappear);
        }

        public void SetTradeItem(Dictionary<ItemSO, int> tradeItems)
        {
            _tradeItems = tradeItems as SerializedDictionary<ItemSO, int>;
        }

        public bool ReceiveItem(ItemSO item, int amount)
        {
            if (_tradeItems.ContainsKey(item))
            {
                _tradeItems[item] -= amount;
                if (_tradeItems[item] <= 0)
                {
                    _tradeItems.Remove(item);
                }
                return true;
            }

            Debug.LogWarning($"Item {item.name} not found in trade items.");
            return false;
        }
    }
}