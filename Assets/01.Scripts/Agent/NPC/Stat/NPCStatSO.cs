using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent
{
    [CreateAssetMenu(fileName = "NPCStatSO", menuName = "SO/NPCStatSO")]
    public class NPCStatSO : ScriptableObject
    {
        public List<NPCStatData> Stats;
        
        public NPCStatData GetStat(NPCStatType type)
        {
            return Stats.Find(stat => stat.Type == type);
        }
    }
}