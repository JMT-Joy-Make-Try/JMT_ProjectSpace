using JMT.DayTime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.DayTime
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dayText, timeText;
        [SerializeField] private Image icon, back;

        [Header("Daytime")]
        [SerializeField] private Sprite sun;
        [SerializeField] private Sprite moon;

        public void ChangeTimeText(int m, int s)
        {
            timeText.text = m.ToString("D2") + ":" + s.ToString("D2");
        }

        public void ChangeDayText(int day)
        {
            dayText.text = "Day " + day;
        }

        public void ChangeDayTime(DaytimeType dayTime)
        {
            switch (dayTime)
            {
                case DaytimeType.Day:
                    icon.sprite = sun;
                    break;
                case DaytimeType.Night:
                    icon.sprite = moon;
                    break;
            }
        }
    }
}
