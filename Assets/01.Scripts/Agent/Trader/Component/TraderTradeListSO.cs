using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.Trader
{
    [CreateAssetMenu(fileName = "TraderTradeList", menuName = "SO/Trader/TraderTradeListSO")]
    public class TraderTradeListSO : ScriptableObject
    {
        public List<TraderTradeSO> traderTrades;
    }
}