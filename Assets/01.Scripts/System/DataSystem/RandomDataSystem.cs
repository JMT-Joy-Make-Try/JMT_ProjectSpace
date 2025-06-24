using JMT.Agent.Trader;
using UnityEngine;

namespace JMT.DataSystem
{
    public class RandomDataSystem : MonoSingleton<RandomDataSystem>
    {
        [SerializeField] private VillageListSO villageListSO;
        [SerializeField] private TraderTradeListSO traderTradeListSO;

        public VillageSO GetRandomVillageSO()
        {
            int random = Random.Range(0, villageListSO.Villages.Count);
            return villageListSO.Villages[random];
        }
        
        public TraderTradeSO GetRandomTraderTradeSO()
        {
            int random = Random.Range(0, traderTradeListSO.traderTrades.Count);
            return traderTradeListSO.traderTrades[random];
        }
    }
}
