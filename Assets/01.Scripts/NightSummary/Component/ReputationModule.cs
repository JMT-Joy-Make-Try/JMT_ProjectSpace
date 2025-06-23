using JMT.Agent;
using JMT.Core;
using System;
using UnityEngine;

namespace JMT.NightSummary.Component
{
    // 평판
    [Serializable]
    public class ReputationModule : IResetable
    {
        [SerializeField] private NPCCollectModule _npcCollectModule;
        //평판은 일꾼의 만족도 평균
        public ReputationModule(NPCCollectModule npcCollectModule)
        {
            _npcCollectModule = npcCollectModule;
        }
        
        public float CalculateReputation()
        {
            if (_npcCollectModule == null) return 0f;
            
            float totalSatisfaction = 0f;
            var npcCount = _npcCollectModule.GetCollectedNPCs();
            
            if (npcCount.Count == 0) return 0f;
            
            foreach (var npc in npcCount)
            {
                totalSatisfaction += npc.StatSO.GetStat(NPCStatType.Satisfaction).GetValue();
            }
            
            return totalSatisfaction / npcCount.Count;
        }

        public void Reset()
        {
            if (_npcCollectModule != null)
            {
                _npcCollectModule.Reset();
            }
        }
    }
}