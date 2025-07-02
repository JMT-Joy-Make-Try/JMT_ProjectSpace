using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Result
{
    public class ResultContentUI : MonoBehaviour
    {
        [SerializeField] private LayoutElement circleImage;
        [SerializeField] private CanvasGroup dayGroup;
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private CanvasGroup goalGroup;

        [Header("Size Settings")]
        [SerializeField] private int generalSize = 80;
        [SerializeField] private int todaySize = 100;
        [SerializeField] private int goalSize = 120;

        [SerializeField] private bool isGoalTest;
        [SerializeField] private bool isTodayTest;

        [Header("Size Animation Settings")]
        [SerializeField] private AnimationCurve sizeCurve;
        [SerializeField] private float duration = 1f;

        public void SetDayContent(string dayDesc = null, bool isToday = false, bool isGoal = false)
        {
            if (isToday)
                SetTodayContent(dayDesc, isGoal);
            else
                SetGeneralContent(isGoal);
        }

        private void SetTodayContent(string dayDesc, bool isGoal = false)
        {
            dayGroup.DOFade(1, 0.3f);
            dayText.text = dayDesc;
            if (isGoal) SetGoalContent();
            else SetCircleSize(todaySize);
        }

        private void SetGeneralContent(bool isGoal = false)
        {
            dayGroup.alpha = 0;
            goalGroup.alpha = 0;
            if (isGoal) SetGoalContent();
            else SetCircleSize(generalSize);
        }

        private void SetGoalContent()
        {
            goalGroup.DOFade(1, 0.3f);
            // Goal Content 수정
            SetCircleSize(goalSize);
        }

        private void SetCircleSize(int value)
        {
            Vector2 size = new(value, value);
            circleImage.DOMinSize(size, duration).SetEase(sizeCurve);
        }
    }
}
