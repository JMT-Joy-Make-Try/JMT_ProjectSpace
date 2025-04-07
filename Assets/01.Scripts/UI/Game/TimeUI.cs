using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dayText, timeText;
        [SerializeField] private Image icon, back;

        [Header("Daytime")]
        [SerializeField] private Color daytextColor;
        [SerializeField] private Color daybackColor;
        [SerializeField] private Color nighttextColor, nightbackColor;
        [SerializeField] private Sprite sun, moon;

        private void Awake()
        {
            DaySystem.Instance.OnChangeTimeEvent += HandleChangeTimeEvent;
            DaySystem.Instance.OnChangeDaytimeEvent += HandleChangeDaytimeEvent;
            DaySystem.Instance.OnChangeDayCountEvent += HandleChangeDayCountEvent;
        }

        private void OnDestroy()
        {
            if (DaySystem.Instance == null) return;
            DaySystem.Instance.OnChangeTimeEvent -= HandleChangeTimeEvent;
            DaySystem.Instance.OnChangeDaytimeEvent -= HandleChangeDaytimeEvent;
            DaySystem.Instance.OnChangeDayCountEvent -= HandleChangeDayCountEvent;
        }

        private void HandleChangeTimeEvent(int m, int s)
        {
            timeText.text = m.ToString("D2") + ":" + s.ToString("D2");
        }

        private void HandleChangeDaytimeEvent(DaytimeType type)
        {
            ChangeDayTime(type);
        }

        private void HandleChangeDayCountEvent(int day)
        {
            dayText.text = "Day " + day;
        }

        private void ChangeDayTime(DaytimeType dayTime)
        {
            switch (dayTime)
            {
                case DaytimeType.Day:
                    icon.sprite = sun;
                    dayText.DOColor(daytextColor, 0.3f);
                    timeText.DOColor(daytextColor, 0.3f);
                    back.DOColor(daybackColor, 0.3f);
                    break;
                case DaytimeType.Night:
                    icon.sprite = moon;
                    dayText.DOColor(nighttextColor, 0.3f);
                    timeText.DOColor(nighttextColor, 0.3f);
                    back.DOColor(nightbackColor, 0.3f);
                    break;
            }
        }
    }
}
