using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.QuestSystem
{
    [CreateAssetMenu(fileName = "NewQuest", menuName = "SO/Data/Quests/QuestSO")]
    public class QuestSO : ScriptableObject
    {
        public string questName;
        public string description;
        public string dialogueRange;
        public List<QuestRewardType> questRewardTypes;
    }

    public enum QuestRewardType
    {
        NPC,
    }
}