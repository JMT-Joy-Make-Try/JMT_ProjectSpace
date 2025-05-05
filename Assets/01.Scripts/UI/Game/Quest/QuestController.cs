using JMT.QuestSystem;
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
        }

        private void OnDestroy()
        {
            QuestManager.Instance.OnQuestStartEvent -= HandleQuestStartEvent;
            QuestManager.Instance.OnQuestEndEvent -= view.CloseUI;
        }

        private void HandleQuestStartEvent(QuestSO SO)
        {
            view.SetQuestView(SO);
        }
    }
}
