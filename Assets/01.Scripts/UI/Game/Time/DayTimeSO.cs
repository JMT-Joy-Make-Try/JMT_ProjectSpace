using System;
using UnityEngine;

namespace JMT.DayTime
{
    [Serializable]
    public struct TimeData
    {
        public int minute;
        public int second;

        public int GetSecond() => minute * 60 + second;
    }

    public enum DaytimeType
    {
        Day,
        Night,
    }

    [CreateAssetMenu(fileName = "TimeSO", menuName = "SO/Data/DayTimeSO")]
    public class DayTimeSO : ScriptableObject
    {
        public TimeData repeatDayTime;
        public TimeData repeatNightTime;
    }
}
