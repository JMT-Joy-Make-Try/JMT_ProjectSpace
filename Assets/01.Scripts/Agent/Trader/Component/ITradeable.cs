using JMT.Item;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public interface ITradeable
    {
        Sprite TradeIllust { get; }
        string Description { get; }
        string ReceiveText { get; }
        ItemSO ReceiveItem { get; }
        List<ItemSO> NeedItems { get; }
    }
}