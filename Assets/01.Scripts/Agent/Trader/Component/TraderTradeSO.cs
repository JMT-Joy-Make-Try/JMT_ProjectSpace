using JMT.Core.Tool.PoolManager.Core;
using JMT.Core.Tool.PoolManager;
using JMT.Item;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;

namespace JMT.Agent.Trader
{
    [CreateAssetMenu(fileName = "TraderTradeSO", menuName = "SO/Trader/TradeSO")]
    public class TraderTradeSO : ScriptableObject, ITradeable
    {
        public event Action OnStartTradeEvent;

        [SerializeField] private Sprite tradeIllust;

        [TextArea(2, 1), Tooltip("행상인 상세설명")]
        [SerializeField] private string description;

        [Space(10), Tooltip("물품 SO")]
        [SerializeField] private ItemSO workerSO;

        [Space(10), Tooltip("추가되는 물품 수")]
        [SerializeField] private int receiveCount;

        [Space(10), Tooltip("필요한 자원")]
        [SerializeField] private SerializedDictionary<ItemSO, int> needItems;

        public string TitleText => "행상인";
        public Sprite TradeIllust => tradeIllust;
        public string Description => description;
        public string ReceiveText => "판매하는 물품";
        public ItemSO ReceiveItem => workerSO;
        public int ReceiveCount => receiveCount;
        public Dictionary<ItemSO, int> NeedItems => needItems;


        public void AddNpc()
        {
            for (int i = 0; i < ReceiveCount; i++)
            {
                AgentManager.Instance.AddNpc();
            }
            PoolingManager.Instance.ResetPool(PoolingType.Agent_NPC);
        }

        public void SucessTrade()
        {
            // 거래 성립시
            OnStartTradeEvent?.Invoke();
            Debug.Log("jklfdsajkl;fdsajkl");
        }
    }
}