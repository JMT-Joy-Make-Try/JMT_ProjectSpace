using System.Collections.Generic;
using UnityEngine;

namespace JMT.QuestSystem
{
    [CreateAssetMenu(fileName = "ChapterSO", menuName = "Quests/ChapterSO")]
    public class ChapterSO : ScriptableObject
    {
        public List<QuestSO> quests;
    }
}
