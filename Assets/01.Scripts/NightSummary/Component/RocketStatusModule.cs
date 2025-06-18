using System;
using UnityEngine;

namespace JMT.NightSummary.Component
{
    // 우주선의 완성도
    [Serializable]
    public class RocketStatusModule
    {
        [SerializeField] private int _rocketCompletionPercent;
        private string _percentText;
        
        public event Action<string> OnRocketCompletionPercentChanged;
        
        public void UpgradeRocketCompletion(int percent)
        {
            _rocketCompletionPercent = Mathf.Clamp(_rocketCompletionPercent + percent, 0, 100);
            _percentText = $"{_rocketCompletionPercent}%";
            
            OnRocketCompletionPercentChanged?.Invoke(_percentText);
        }
    }
}