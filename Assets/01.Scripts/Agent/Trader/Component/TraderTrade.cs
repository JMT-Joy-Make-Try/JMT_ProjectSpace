using JMT.Core;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Object;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public class TraderTrade : MonoBehaviour, ITraderComponent, IItemReceivable
    {
        public Trader Trader { get; private set; }
        
        private TraderTradeSO _traderTradeSO;
        
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

        public void SetTradeItem(TraderTradeSO traderTradeSO)
        {
            _traderTradeSO = Instantiate(traderTradeSO);
        }

        public bool ReceiveItem(ItemSO item, int amount)
        {
            if (_traderTradeSO == null)
            {
                Debug.LogWarning("TraderTradeSO is not set.");
                return false;
            }
            
            if (_traderTradeSO.NeedItems.Contains(item))
            {
                _traderTradeSO.NeedItems.Remove(item);
                if (_traderTradeSO.NeedItems.Count == 0)
                {
                    DropItem();
                }
                else
                {
                    Debug.Log($"Received item: {item.name}, remaining items needed: {_traderTradeSO.NeedItems.Count}");
                }
                return true;
            }
            Debug.LogWarning($"Item {item.name} not found in trade items.");
            return false;
        }

        private void DropItem()
        {
            var item = PoolingManager.Instance.Pop(PoolingType.Item) as ItemObject;
            
            if (item == null)
            {
                Debug.LogError("Failed to pop item from pool.");
                return;
            }
            
            item.SetItem(_traderTradeSO.ReceiveItem);
            item.transform.position = Trader.transform.position + new Vector3(0, 1, 0);
            item.IsCollectable = true;
            Trader.StateMachineCompo.ChangeStateDelay(TraderStateEnum.Disappear, 0.5f);
        }
    }
}