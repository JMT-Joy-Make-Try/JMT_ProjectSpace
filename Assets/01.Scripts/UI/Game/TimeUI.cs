using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public enum Daytime
    {
        Day,
        Night,
    }
    [Serializable]
    public struct TimeData
    {
        public int minute;
        public int second;
    }
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
        private int day;
        private bool isNight;

        public void StartDayTime()
        {
            ChangeDayTime(Daytime.Day);
            timeRoutine = StartCoroutine(TimeCoroutine(repeatDayTime));
        }

        public void StartNightTime()
        {
            ChangeDayTime(Daytime.Night);
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
                dayText.text = "Day " + day;
                timeText.text = saveTime.minute.ToString("D2") + ":" + saveTime.second.ToString("D2");
                yield return waitTime;

                if (saveTime.second <= 0)
                {
                    if (saveTime.minute <= 0)
                    {
                        if (isNight)
                        {
                            day++;
                            isNight = false;
                            saveTime.minute = repeatDayTime.minute;
                            saveTime.second = repeatDayTime.second;
                            ChangeDayTime(Daytime.Day);
                        }
                        else
                        {
                            isNight = true;
                            saveTime.minute = repeatNightTime.minute;
                            saveTime.second = repeatNightTime.second;
                            ChangeDayTime(Daytime.Night);
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

        private void ChangeDayTime(Daytime dayTime)
        {
            switch (dayTime)
            {
                case Daytime.Day:
                    icon.sprite = sun;
                    dayText.DOColor(daytextColor, 0.3f);
                    timeText.DOColor(daytextColor, 0.3f);
                    back.DOColor(daybackColor, 0.3f);
                    break;
                case Daytime.Night:
                    icon.sprite = moon;
                    dayText.DOColor(nighttextColor, 0.3f);
                    timeText.DOColor(nighttextColor, 0.3f);
                    back.DOColor(nightbackColor, 0.3f);
                    break;
            }
        }
    }
}
