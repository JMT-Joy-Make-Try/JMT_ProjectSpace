using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dayText, timeText;
        [SerializeField] private TimeData repeatDayTime, repeatNightTime;
        [SerializeField] private Image icon, back;

        [Header("Daytime")]
        [SerializeField] private Color daytextColor;
        [SerializeField] private Color daybackColor;
        [SerializeField] private Color nighttextColor, nightbackColor;
        [SerializeField] private Sprite sun, moon;

        private Coroutine timeRoutine;
        private TimeData saveTime;
        private bool isNight;

        private void Start()
        {
            StartDayTime();
        }

        public void StartDayTime()
        {
            ChangeDayTime(DaytimeType.Day);
            timeRoutine = StartCoroutine(TimeCoroutine(repeatDayTime));
        }

        public void StartNightTime()
        {
            ChangeDayTime(DaytimeType.Night);
            timeRoutine = StartCoroutine(TimeCoroutine(repeatNightTime));
        }

        public void StopTime()
        {
            StopCoroutine(timeRoutine);
        }

        public void RestartTime()
        {
            timeRoutine = StartCoroutine(TimeCoroutine(saveTime));
        }

        private IEnumerator TimeCoroutine(TimeData time)
        {
            var waitTime = new WaitForSeconds(1);
            saveTime.minute = time.minute;
            saveTime.second = time.second;
            while (true)
            {
                dayText.text = "Day " + DaySystem.Instance.DayCount;
                timeText.text = saveTime.minute.ToString("D2") + ":" + saveTime.second.ToString("D2");
                yield return waitTime;

                if (saveTime.second <= 0)
                {
                    if (saveTime.minute <= 0)
                    {
                        if (isNight)
                        {
                            DaySystem.Instance.AddDayCount();
                            isNight = false;
                            saveTime.minute = repeatDayTime.minute;
                            saveTime.second = repeatDayTime.second;
                            ChangeDayTime(DaytimeType.Day);
                            DaySystem.Instance.ChangeDay();
                        }
                        else
                        {
                            isNight = true;
                            saveTime.minute = repeatNightTime.minute;
                            saveTime.second = repeatNightTime.second;
                            ChangeDayTime(DaytimeType.Night);
                            DaySystem.Instance.ChangeNight();
                        }
                    }
                    else
                    {
                        saveTime.minute--;
                        saveTime.second += 59;
                    }
                }
                else saveTime.second--;
            }
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
