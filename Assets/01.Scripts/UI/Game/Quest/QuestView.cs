using JMT.QuestSystem;
using TMPro;
using UnityEngine;

namespace JMT.UISystem.Quest
{
    public class QuestView : PanelUI
    {
        [SerializeField] private TextMeshProUGUI questNameText;
        [SerializeField] private TextMeshProUGUI questDescText;

        public void SetQuestView(QuestSO quest)
        {
            if(!IsOpen) OpenUI();
            questNameText.text = quest.questName;
            questDescText.text = quest.description;
        }
    }
}
