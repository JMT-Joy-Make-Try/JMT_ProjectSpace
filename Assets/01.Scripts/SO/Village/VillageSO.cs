using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Agent.Trader;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using System.Collections.Generic;
using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "Village", menuName = "SO/Data/Village/VillageSO")]
    public class VillageSO : ScriptableObject, ITradeable
    {
        [SerializeField] private Sprite tradeIllust;

        [TextArea(2, 1), Tooltip("촌락 상세설명")]
        [SerializeField] private string description;

        [Space(10), Tooltip("일꾼 SO")]
        [SerializeField] private ItemSO workerSO;

        [Space(10), Tooltip("추가되는 일꾼 수")]
        [SerializeField] private int AddWorkerCount;

        [Space(10), Tooltip("필요한 자원")]
        [SerializeField] private SerializedDictionary<ItemSO, int> needItems;

        public string TitleText => "촌락";
        public Sprite TradeIllust => tradeIllust;
        public string Description => description;
        public string ReceiveText => "추가되는 일꾼";
        public ItemSO ReceiveItem => workerSO;
        public int ReceiveCount => AddWorkerCount;
        public Dictionary<ItemSO, int> NeedItems => needItems;


        public void AddNpc()
        {
            for (int i = 0; i < AddWorkerCount; i++)
            {
                AgentManager.Instance.AddNpc();
            }
            PoolingManager.Instance.ResetPool(PoolingType.Agent_NPC);
        }

        public void SucessTrade()
        {
            AddNpc();
        }
    }
}
