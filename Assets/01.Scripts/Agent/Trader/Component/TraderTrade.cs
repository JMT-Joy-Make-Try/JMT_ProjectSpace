using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public class TraderTrade : MonoBehaviour, ITraderComponent, IItemReceivable
    {
        public Trader Trader { get; private set; }
        
        [SerializeField] private SerializedDictionary<ItemSO, int> _tradeItems = new();
        [SerializeField] private SerializedDictionary<ItemSO, int> _dropItems = new SerializedDictionary<ItemSO, int>();
        
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
                if (_tradeItems.Count <= 0)
                {
                    DropItem();
                }
                return true;
            }

            Debug.LogWarning($"Item {item.name} not found in trade items.");
            return false;
        }

        private void DropItem()
        {
            foreach (var items in _dropItems)
            {
                for (int i = 0; i < items.Value; i++)
                {
                    var itemObj = PoolingManager.Instance.Pop(PoolingType.Item) as ItemObject;
                    itemObj.SetItem(items.Key);
                    itemObj.transform.position = Trader.transform.position + Vector3.up * 0.5f;
                    itemObj.IsCollectable = true;
                }
            }
            
            Trader.StateMachineCompo.ChangeStateDelay(TraderStateEnum.Disappear, 0.5f);
        }
    }
}