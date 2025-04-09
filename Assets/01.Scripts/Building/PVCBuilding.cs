using JMT.UISystem;
using UnityEngine;

namespace JMT
{
    public class PVCBuilding : MonoBehaviour
    {
        private FillBarUI fillBarUI;
        private PVCUI pvcUI;

        private void Awake()
        {
            fillBarUI = GetComponent<FillBarUI>();
            pvcUI = GetComponent<PVCUI>();
        }

        public void SetBuildTime(TimeData timeData)
        {
            int secTime = timeData.GetSecond();
            fillBarUI.ResetBar(0);
            fillBarUI.SetHpBar(1, 1, secTime);
            pvcUI.SetTime(secTime);
        }
    }
}
