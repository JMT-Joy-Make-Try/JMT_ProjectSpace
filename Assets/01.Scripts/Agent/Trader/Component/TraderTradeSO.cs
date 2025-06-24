using JMT.Item;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.Trader
{
    [CreateAssetMenu(fileName = "TraderTradeSO", menuName = "SO/Trader/TradeSO")]
    public class TraderTradeSO : ScriptableObject, ITradeable
    {
        [field: SerializeField] public Sprite TradeIllust { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public string ReceiveText { get; private set; }
        [field: SerializeField] public ItemSO ReceiveItem { get; private set; }
        [field: SerializeField] public List<ItemSO> NeedItems {  get; private set; } = new List<ItemSO>();
    }
}