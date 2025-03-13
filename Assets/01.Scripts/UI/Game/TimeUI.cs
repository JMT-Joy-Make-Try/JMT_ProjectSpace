using System.Collections;
using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dayText, timeText;
        [SerializeField] private int repeatMinute, repeatSecond;

        private Coroutine timeRoutine;
        private int day, saveMinute, saveSecond;

        public void StartTime()
        {
            timeRoutine = StartCoroutine(TimeCoroutine(repeatMinute, repeatSecond));
        }

        public void StopTime()
        {
            StopCoroutine(timeRoutine);
        }

        public void RestartTime()
        {
            timeRoutine = StartCoroutine(TimeCoroutine(saveMinute, saveSecond));
        }

        private IEnumerator TimeCoroutine(int m, int s)
        {
            var waitTime = new WaitForSeconds(1);
            saveMinute = m;
            saveSecond = s;
            while (true)
            {
                dayText.text = "Day " + day;
                timeText.text = saveMinute.ToString("D2") + ":" + saveSecond.ToString("D2");
                yield return waitTime;

                if (saveSecond <= 0)
                {
                    if (saveMinute <= 0)
                    {
                        saveMinute = repeatMinute;
                        saveSecond = repeatSecond;
                        day++;
                    }
                    else
                    {
                        saveMinute--;
                        saveSecond += 59;
                    }
                }
                else saveSecond--;
            }
        }
    }
}
