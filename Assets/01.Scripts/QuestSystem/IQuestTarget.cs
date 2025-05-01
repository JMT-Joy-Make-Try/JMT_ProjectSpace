using UnityEngine;

namespace JMT.QuestSystem
{
    public interface IQuestTarget
    {
        QuestSO QuestData { get; }
        QuestState QuestState { get; }
        bool IsActive { get; }
        void RunQuest();
        void Enable();

        public void SetState(QuestState state);
    }

}