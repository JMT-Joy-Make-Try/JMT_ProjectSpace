using JMT.QuestSystem;
using System;
using UnityEngine;

namespace JMT.UISystem.Quest
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField] private QuestView view;

        private void Awake()
        {
            QuestManager.Instance.OnQuestStartEvent += HandleQuestStartEvent;
            QuestManager.Instance.OnQuestEndEvent += view.CloseUI;
            QuestManager.Instance.OnQuestCountEvent += HandleQuestCountEvent;
        }

        private void OnDestroy()
        {
            QuestManager.Instance.OnQuestStartEvent -= HandleQuestStartEvent;
            QuestManager.Instance.OnQuestEndEvent -= view.CloseUI;
            QuestManager.Instance.OnQuestCountEvent -= HandleQuestCountEvent;
        }

        private void HandleQuestStartEvent(QuestSO SO)
        {
            view.SetQuestView(SO);
            
        }

        private void HandleQuestCountEvent(string count)
        {
            view.SetQuestNameCount(count);
        }
    }
}
