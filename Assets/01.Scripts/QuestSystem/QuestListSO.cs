using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.QuestSystem
{
    [CreateAssetMenu(fileName = "QuestList", menuName = "SO/QuestListSO")]
    public class QuestListSO : ScriptableObject
    {
        public List<QuestSO> quests = new List<QuestSO>();
    }
}