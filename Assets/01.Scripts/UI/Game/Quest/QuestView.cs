using DG.Tweening;
using JMT.Core.Tool;
using JMT.QuestSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Quest
{
    public class QuestView : PanelUI
    {
        [SerializeField] private TextMeshProUGUI questNameText;
        [SerializeField] private TextMeshProUGUI questCountText;
        [SerializeField] private TextMeshProUGUI questDescText;

        [Header("Color Settings")]
        [SerializeField] private Color questColor;
        [SerializeField] private Color completeColor;
        [SerializeField] private Sprite ping, check;
        [SerializeField] private Image verticalLine, horizontalLine;
        [SerializeField] private Image pingIcon;

        public void SetQuestView(QuestSO quest)
        {
            if(!IsOpen) OpenUI();
            questNameText.text = quest.questName;
            questDescText.text = quest.description;

            verticalLine.color = questColor;
            horizontalLine.color = questColor;
            pingIcon.sprite = ping;
        }

        public void SetQuestNameCount(string count)
        {
            questCountText.text = $"({count})";
        }

        public void QuestComplete()
        {
            verticalLine.DOColor(completeColor, 0.3f);
            horizontalLine.DOColor(completeColor, 0.3f);
            pingIcon.sprite = check;
        }
    }
}
