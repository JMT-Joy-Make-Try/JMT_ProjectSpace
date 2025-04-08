using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace JMT
{
    public enum DaytimeType
    {
        Day,
        Night,
    }
    [Serializable]
    public struct TimeData
    {
        public int minute;
        public int second;

        public int GetSecond() => minute * 60 + second;
    }

    public class DaySystem : MonoSingleton<DaySystem>
    {
        
        [SerializeField] private TimeData repeatDayTime, repeatNightTime;

        public event Action<int> OnChangeDayCountEvent;
        public event Action<int, int> OnChangeTimeEvent;
        public event Action<DaytimeType> OnChangeDaytimeEvent;

        private int dayCount = 0;
        private DaytimeType daytimeType;

        public int DayCount => dayCount;
        public DaytimeType DayTimeType => daytimeType;

        private Coroutine timeRoutine;
        private TimeData saveTime;
        private bool isNight;

        private void Start()
        {
            StartDayTime();
        }

        public void AddDayCount()
        {
            dayCount++;
            OnChangeDayCountEvent?.Invoke(dayCount);
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
            AddDayCount();
            var waitTime = new WaitForSeconds(1);
            saveTime.minute = time.minute;
            saveTime.second = time.second;
            while (true)
            {
                OnChangeTimeEvent?.Invoke(saveTime.minute, saveTime.second);
                yield return waitTime;

                if (saveTime.second <= 0)
                {
                    if (saveTime.minute <= 0)
                    {
                        if (isNight)
                        {
                            AddDayCount();
                            isNight = false;
                            saveTime.minute = repeatDayTime.minute;
                            saveTime.second = repeatDayTime.second;
                            ChangeDayTime(DaytimeType.Day);
                        }
                        else
                        {
                            isNight = true;
                            saveTime.minute = repeatNightTime.minute;
                            saveTime.second = repeatNightTime.second;
                            ChangeDayTime(DaytimeType.Night);
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

        public void ChangeDayTime(DaytimeType type)
        {
            daytimeType = type;
            OnChangeDaytimeEvent?.Invoke(daytimeType);
        }
    }
}
