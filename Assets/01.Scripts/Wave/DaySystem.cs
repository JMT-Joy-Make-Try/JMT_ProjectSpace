using JMT.UISystem;
using System;
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
    }

    public class DaySystem : MonoSingleton<DaySystem>
    {
        public event Action<int> OnChangeDayCountEvent;
        public event Action OnDayEvent;
        public event Action OnNightEvent;

        private int dayCount = 1;
        private DaytimeType daytimeType;

        public int DayCount => dayCount;
        public DaytimeType DayTimeType => daytimeType;

        public void AddDayCount()
        {
            dayCount++;
            OnChangeDayCountEvent?.Invoke(dayCount);
        }

        public void ChangeNight()
        {
            daytimeType = DaytimeType.Night;
            OnNightEvent?.Invoke();
        }

        public void ChangeDay()
        {
            daytimeType = DaytimeType.Day;
            OnDayEvent?.Invoke();
        }
    }
}
