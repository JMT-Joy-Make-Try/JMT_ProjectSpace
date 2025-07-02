using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.UISystem.Result
{
    public class ResultCountView : MonoBehaviour
    {
        [SerializeField] private Transform resultCountTrm;

        private List<ResultContentUI> results;

        private void Awake()
        {
            results = resultCountTrm.GetComponentsInChildren<ResultContentUI>().ToList();
        }

        public void SetDay(int day)
        {
            int multiDayValue = day / 5;
            for(int i = 0; i < results.Count; ++i)
            {
                int currentDay = multiDayValue * 5 + i + 1;
                string dayText = $"Day{currentDay}";

                results[i].SetDayContent(dayText, currentDay == day, currentDay % 5 == 0);
            }
        }
    }
}
