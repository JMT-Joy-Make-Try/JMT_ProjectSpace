using JMT.DayTime;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.UISystem.DayTime
{
    public class TimeModel
    {
        public event Action<int> OnChangeDayCountEvent;
        public event Action<int, int> OnChangeTimeEvent;
        public event Action<DaytimeType> OnChangeDaytimeEvent;

        private DaytimeType daytimeType;
        private TimeData saveTime;

        private int dayCount = 0;
        private bool isNight;

        public void AddDayCount()
        {
            dayCount++;
            OnChangeDayCountEvent?.Invoke(dayCount);
        }
        public void SetTime(TimeData time)
        {
            saveTime = time;
        }

        public void ChangeDayTime(DaytimeType type)
        {
            daytimeType = type;
            OnChangeDaytimeEvent?.Invoke(daytimeType);
        }

        public void ChangeTime(DayTimeSO timeSO)
        {
            if (--saveTime.second < 0)
            {
                if (--saveTime.minute < 0)
                {
                    if (isNight)
                    {
                        AddDayCount();
                        isNight = false;
                        saveTime = timeSO.repeatDayTime;
                        ChangeDayTime(DaytimeType.Day);
                    }
                    else
                    {
                        isNight = true;
                        saveTime = timeSO.repeatNightTime;
                        ChangeDayTime(DaytimeType.Night);
                    }
                }
                else
                {
                    saveTime.second = 59;
                }
            }

            OnChangeTimeEvent?.Invoke(saveTime.minute, saveTime.second);
        }
    }
}
