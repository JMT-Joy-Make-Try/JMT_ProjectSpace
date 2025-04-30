using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.QuestSystem
{
    [CreateAssetMenu(fileName = "QuestList", menuName = "SO/QuestListSO")]
    public class QuestListSO : ScriptableObject, ICloneable
    {
        public List<QuestSO> quests = new List<QuestSO>();

        /// <summary>
        /// 퀘스트 리스트에서 퀘스트를 이름으로 검색합니다.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>찾은 퀘스트</returns>
        public QuestSO GetQuest(string name)
        {
            QuestSO quest = quests.FirstOrDefault(q => q.name == name);
            if (quest == null)
            {
                Debug.LogError($"Quest {name} not found");
                return null;
            }
            
            return quest;
        }
        
        /// <summary>
        /// 퀘스트 리스트에서 첫 번째 퀘스트를 가져옵니다.
        /// </summary>
        /// <returns>첫 번쨰 퀘스트</returns>
        public QuestSO GetFirstQuest()
        {
            if (quests.Count == 0)
            {
                Debug.LogError("No quests available");
                return null;
            }
            
            return quests[0];
        }

        public object Clone()
        {
            return Instantiate(this);
        }
    }
}