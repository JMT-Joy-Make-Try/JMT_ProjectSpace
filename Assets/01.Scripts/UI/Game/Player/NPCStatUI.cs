using UnityEngine;

namespace JMT.UISystem
{
    public class NPCStatUI : WorldCanvasUI
    {
        [SerializeField] private GameObject health, oxygen;

        private void Awake()
        {
            SetHealthStat(false);
            SetOxygenStat(false);
        }

        public void SetHealthStat(bool isHealthWarning)
        {
            health.SetActive(isHealthWarning);
        }

        public void SetOxygenStat(bool isOxygenWarning)
        {
            oxygen.SetActive(isOxygenWarning);
        }
    }
}
