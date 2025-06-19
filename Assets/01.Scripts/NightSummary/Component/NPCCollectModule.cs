using JMT.Agent;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.NightSummary.Component
{
    // 포섭된 NPC
    [Serializable]
    public class NPCCollectModule : IResetable
    {
        [SerializeField] private List<NPCStat> _npcList = new List<NPCStat>();
        
        
        public void CollectNPC(NPCStat npc)
        {
            if (npc == null) return;

            _npcList.Add(npc);
        }

        public List<NPCStat> GetCollectedNPCs()
        {
            return _npcList;
        }

        public void Reset()
        {
            _npcList.Clear();
        }
    }
}