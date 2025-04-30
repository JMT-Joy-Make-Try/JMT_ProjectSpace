using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class QuestSO : ScriptableObject, ICloneable
    {
        public string questName;
        public string questDescription;
        public bool isCompleted;
        public event Action OnQuestCompleted;
        
        public object Clone()
        {
            return Instantiate(this);
        }

        /// <summary>
        /// 퀘스트를 진행하는 메서드
        /// </summary>
        public virtual void RunQuest()
        {
            if (isCompleted)
                CompleteQuest();
            
            // todo: 퀘스트 진행 로직을 여기에 추가합니다.
        }
        
        /// <summary>
        /// 퀘스트 완료 시 호출되는 메서드
        /// </summary>
        protected virtual void CompleteQuest()
        {
            OnQuestCompleted?.Invoke();
            Debug.Log($"Quest '{questName}' completed!");
        }
    }
}