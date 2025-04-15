using System;
using UnityEngine;

namespace JMT.Object
{
    public class VisibilityTracker : MonoBehaviour
    {
        public event Action OnInvisibleCallback;

        private void OnBecameInvisible()
        {
            Debug.LogWarning("dadds");
            OnInvisibleCallback?.Invoke();
        }
    }
}