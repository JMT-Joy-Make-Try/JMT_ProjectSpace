using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace JMT.CameraSystem
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource _mainImpulseSource;

        public void ShakeCamera(float strength)
        {
            if (_mainImpulseSource != null)
            {
                _mainImpulseSource.GenerateImpulse(strength);
            }
        }

        public void ShakeCamera(float strength, float duration)
        {
            StartCoroutine(ImpulseCoroutine(strength, duration));
        }

        private IEnumerator ImpulseCoroutine(float strength, float duration)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _mainImpulseSource.GenerateImpulse(strength);
                yield return null;
            }
        }
    }
}