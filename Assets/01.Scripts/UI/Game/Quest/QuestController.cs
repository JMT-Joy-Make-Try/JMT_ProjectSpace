using DG.Tweening;
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
            QuestManager.Instance.OnQuestEndEvent += HandleQuestEndEvent;
            QuestManager.Instance.OnQuestCountEvent += HandleQuestCountEvent;
        }

        private void OnDestroy()
        {
            QuestManager.Instance.OnQuestStartEvent -= HandleQuestStartEvent;
            QuestManager.Instance.OnQuestEndEvent -= HandleQuestEndEvent;
            QuestManager.Instance.OnQuestCountEvent -= HandleQuestCountEvent;
        }

        private void HandleQuestStartEvent(QuestSO SO)
        {
            view.SetQuestView(SO);
            
        }

        private void HandleQuestEndEvent()
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() => view.QuestComplete());
            seq.AppendInterval(0.6f);
            seq.AppendCallback(() => view.CloseUI());
        }

        private void HandleQuestCountEvent(string count)
        {
            view.SetQuestNameCount(count);
        }
    }
}
