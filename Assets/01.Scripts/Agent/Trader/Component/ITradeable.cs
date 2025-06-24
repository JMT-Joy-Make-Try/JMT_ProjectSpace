using AYellowpaper.SerializedCollections;
using JMT.Item;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public interface ITradeable
    {
        string TitleText { get; }
        Sprite TradeIllust { get; }
        string Description { get; }
        string ReceiveText { get; }
        ItemSO ReceiveItem { get; }
        int ReceiveCount { get; }
        Dictionary<ItemSO, int> NeedItems { get; }

        void SucessTrade();
    }
}