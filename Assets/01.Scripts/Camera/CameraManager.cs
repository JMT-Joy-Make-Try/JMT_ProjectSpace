using DG.Tweening;
using JMT.Core.Tool;
using System;
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
        
        public void ShakeCamera(float amplitude, float frequency, float duration)
        {
            _mainImpulseSource.GenerateImpulse(new Vector3(amplitude, frequency, duration));
        }
    }
}
