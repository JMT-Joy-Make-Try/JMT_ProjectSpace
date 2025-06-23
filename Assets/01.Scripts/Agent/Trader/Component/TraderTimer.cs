using System;
using System.Collections;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public class TraderTimer : MonoBehaviour, ITraderComponent
    {
        public Trader Trader { get; private set; }
        
        private float _timer = 0f;
        private float _currentTime = 0f;
        
        public event Action OnTimerComplete;
        
        public void Init(Trader trader)
        {
            Trader = trader;
        }
        
        public void SetTimer(float time)
        {
            _timer = time;
            _currentTime = 0;
        }

        public void StartTimer()
        {
            StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            while (_currentTime < _timer)
            {
                _currentTime += Time.deltaTime;
                yield return null;
            }
            OnTimerComplete?.Invoke();
        }
    }
}