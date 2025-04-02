using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class WorkerUI : MonoBehaviour
    {
        private Image icon, health;
        private TextMeshProUGUI oxygenValueText;

        private void Awake()
        {
            Transform stat = transform.Find("Stat");
            icon = transform.Find("Icon").GetComponent<Image>();
            health = stat.Find("Health").GetComponent<Image>();
            oxygenValueText = stat.Find("Oxygen").GetComponentInChildren<TextMeshProUGUI>();
        }
        public void SetStat()
        {
            // Do something
        }
    }
}
