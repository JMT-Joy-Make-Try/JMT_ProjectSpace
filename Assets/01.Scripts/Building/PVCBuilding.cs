using JMT.UISystem;
using UnityEngine;

namespace JMT
{
    public class PVCBuilding : MonoBehaviour
    {
        private FillBarUI fillBarUI;

        private void Awake()
        {
            fillBarUI = GetComponent<FillBarUI>();
        }

        public void SetBuildTime(TimeData timeData)
        {
            fillBarUI.ResetBar(0);
            fillBarUI.SetHpBar(1, 1, timeData.GetSecond());
        }
    }
}
