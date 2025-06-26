using System;
using UnityEngine;

namespace JMT.Core
{
    public class EventChannelSO<T> : ScriptableObject
    {
        private event Action<T> OnEventRaised;

        public void AddListener(Action<T> listener)
        {
            OnEventRaised += listener;
        }

        public void RemoveListener(Action<T> listener)
        {
            OnEventRaised -= listener;
        }

        public void RaiseEvent(T value)
        {
            OnEventRaised?.Invoke(value);
        }
    }

    public class EventChannelSO : ScriptableObject
    {
        private event Action OnEventRaised;

        public void AddListener(Action listener)
        {
            OnEventRaised += listener;
        }

        public void RemoveListener(Action listener)
        {
            OnEventRaised -= listener;
        }

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}
