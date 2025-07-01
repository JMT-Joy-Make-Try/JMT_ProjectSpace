using JMT.Core;
using System;
using UnityEngine;

namespace JMT.NightSummary.Component
{
    // 우주선의 완성도
    [Serializable]
    public class RocketStatusModule : IResetable
    {
        [SerializeField] private int _rocketCompletionPercent;
        private string _percentText;
        
        public string PercentText => _percentText;
        
        public event Action<string> OnRocketCompletionPercentChanged;

        public RocketStatusModule()
        {
            Reset();
        }

        public void Reset()
        {
            _rocketCompletionPercent = 0;
            _percentText = "0%";
        }

        public void UpgradeRocketCompletion(int percent)
        {
            _rocketCompletionPercent += percent;
            if (_rocketCompletionPercent > 100)
            {
                _rocketCompletionPercent = 100; // 최대 100%로 제한
            }
            _percentText = $"{_rocketCompletionPercent}%";
            
            OnRocketCompletionPercentChanged?.Invoke(_percentText);
        }
    }
}