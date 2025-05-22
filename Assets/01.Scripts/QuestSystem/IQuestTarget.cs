using JMT.Planets.Tile;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.QuestSystem
{
    public interface IQuestTarget
    {
        List<QuestPing> QuestPing { get; }
        QuestSO QuestData { get; }
        QuestState QuestState { get; }
        bool IsActive { get; }
        void RunQuest(int num);
        void Enable();

        public void SetState(QuestState state);
    }

}