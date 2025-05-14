using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.QuestSystem
{
    [CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
    public class QuestSO : ScriptableObject
    {
        public string questName;
        public string description;
        public List<QuestRewardType> questRewardTypes;
    }

    public enum QuestRewardType
    {
        NPC,
    }
}