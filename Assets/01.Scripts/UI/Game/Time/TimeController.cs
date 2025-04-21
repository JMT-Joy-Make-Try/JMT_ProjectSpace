using JMT.DayTime;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.UISystem.DayTime
{
    public class TimeController : MonoBehaviour
    {

        public event Action<int> OnChangeDayCountEvent
        {
            add => model.OnChangeDayCountEvent += value;
            remove => model.OnChangeDayCountEvent -= value;
        }
        public event Action<int, int> OnChangeTimeEvent
        {
            add => model.OnChangeTimeEvent += value;
            remove => model.OnChangeTimeEvent -= value;
        }
        public event Action<DaytimeType> OnChangeDaytimeEvent
        {
            add => model.OnChangeDaytimeEvent += value;
            remove => model.OnChangeDaytimeEvent -= value;
        }

        [SerializeField] private DayTimeSO timeSO;
        [SerializeField] private TimeView view;
        private readonly TimeModel model = new();


        private void Awake()
        {
            model.OnChangeTimeEvent += view.ChangeTimeText;
            model.OnChangeDaytimeEvent += view.ChangeDayTime;
            model.OnChangeDayCountEvent += view.ChangeDayText;
            StartDayTime();
        }

        private void OnDestroy()
        {
            model.OnChangeTimeEvent -= view.ChangeTimeText;
            model.OnChangeDaytimeEvent -= view.ChangeDayTime;
            model.OnChangeDayCountEvent -= view.ChangeDayText;
        }

        public void StartDayTime()
        {
            model.SetTime(timeSO.repeatDayTime);
            model.ChangeDayTime(DaytimeType.Day);
            StartCoroutine(TimeRoutine());
        }

        public void StartNightTime()
        {
            model.SetTime(timeSO.repeatNightTime);
            model.ChangeDayTime(DaytimeType.Night);
            StartCoroutine(TimeRoutine());
        }

        private IEnumerator TimeRoutine()
        {
            var wait = new WaitForSeconds(1f);

            while (true)
            {
                model.ChangeTime(timeSO);
                yield return wait;
            }
        }
    }
}
