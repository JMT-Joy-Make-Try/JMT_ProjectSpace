using JMT.Core;
using JMT.Core.Tool;
using UnityEngine;
using System;
using System.Collections;

namespace JMT.Agent.NPC
{
    public class NPCOxygen : MonoBehaviour, IOxygen
    {
        [field: SerializeField] public int Oxygen { get; private set; }
        [SerializeField] private float _decreaseTime = 1f;
        public event Action<bool> OnOxygenWarningEvent;
        public event Action OnOxygenLowEvent;
        private bool _isOxygenLow = false;
        public bool IsOxygenLow => _isOxygenLow;
        private int _currentOxygen;
        

        private void Start()
        {
            InitOxygen();
        }

        private IEnumerator OxygenDecreaseCoroutine()
        {
            while (!_isOxygenLow)
            {
                yield return new WaitForSeconds(_decreaseTime);
                if (_currentOxygen.GetPercent(Oxygen) >= 10)
                {
                    AddOxygen(-1);
                }
                else
                {
                    _isOxygenLow = true;
                    OnOxygenLowEvent?.Invoke();
                }
            }
        }

        public void AddOxygen(int value)
        {
            _currentOxygen += value;
            int healthPercent = Mathf.RoundToInt(_currentOxygen * 100 / Oxygen);
            if (healthPercent <= 10)
                OnOxygenWarningEvent?.Invoke(true);
            else
                OnOxygenWarningEvent?.Invoke(false);
        }

        public void InitOxygen()
        {
            _currentOxygen = Oxygen;
            _isOxygenLow = false;
            StartCoroutine(OxygenDecreaseCoroutine());
        }
    }
}