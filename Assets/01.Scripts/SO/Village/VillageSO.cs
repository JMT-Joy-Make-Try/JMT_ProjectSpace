using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "Village", menuName = "SO/Data/Village/VillageSO")]
    public class VillageSO : ScriptableObject
    {
        [TextArea(2, 1), Tooltip("촌락 상세설명")]
        public string VillageDescription;

        [Space(10), Tooltip("추가되는 일꾼 수")]
        public int AddWorkerCount;

        [Space(10), Tooltip("필요한 자원")]
        public SerializedDictionary<ItemSO, int> NeedItems;
        
        public void AddNpc()
        {
            for (int i = 0; i < AddWorkerCount; i++)
            {
                AgentManager.Instance.AddNpc();
            }
            PoolingManager.Instance.ResetPool(PoolingType.Agent_NPC);
        }
    }
}
