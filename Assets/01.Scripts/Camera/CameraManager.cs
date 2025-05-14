using DG.Tweening;
using JMT.Core.Tool;
using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace JMT.CameraSystem
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [Header("Camera")]
        [SerializeField] private CinemachineCamera _mainCamera;
        [Space]
        
        [Header("Extension")]
        [SerializeField] private CinemachineImpulseSource _mainImpulseSource;
        
        public CinemachineCamera MainCamera => _mainCamera;
        
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
