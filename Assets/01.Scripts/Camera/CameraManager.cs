using DG.Tweening;
using JMT.Core.Tool;
using JMT.DayTime;
using JMT.UISystem;
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

        public event Action OnCameraZoom;
        private CinemachineFollow _cinemachineFollow;
        
        private Vector3 _defaultCameraPosition;
        private Vector3 _defaultCameraRotation;
        private Vector3 _defaultFollowOffset;
        
        
        public CinemachineCamera MainCamera => _mainCamera;

        protected override void Awake()
        {
            base.Awake();
            if (_mainCamera == null)
            {
                Debug.LogError("Main Camera is not assigned in CameraManager.");
                return;
            }
            
            _defaultCameraRotation = _mainCamera.transform.rotation.eulerAngles;
            _cinemachineFollow = _mainCamera.GetComponent<CinemachineFollow>();
            _defaultFollowOffset = _cinemachineFollow.FollowOffset;
        }

        private void Start()
        {
            GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent += HandleNightEvent;
        }

        private void OnDestroy()
        {
            if (GameUIManager.Instance != null)
            {
                GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent -= HandleNightEvent;
            }
        }

        private void HandleNightEvent(DaytimeType obj)
        {
            if (obj == DaytimeType.Night)
            {
                _mainCamera.DOZoom(16f, 1f);
                OnCameraZoom?.Invoke();
            }
            else if (obj == DaytimeType.Day)
            {
                _mainCamera.DOZoom(14f, 1f);
            }
        }

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
        
        public void RotationCamera(Vector3 rotation, float duration, Ease ease = Ease.Unset)
        {
            if (_mainCamera == null) return;

            _mainCamera.transform.DORotate(rotation, duration)
                .SetEase(ease);
        }

        private void SetFollowOffset(float y, float duration, Ease ease = Ease.Unset)
        {
            if (_cinemachineFollow == null) return;
            //_cinemachineFollow.FollowOffset = new Vector3(_cinemachineFollow.FollowOffset.x, y, _cinemachineFollow.FollowOffset.z);
            _cinemachineFollow.DOFollowOffset(
                new Vector3(_cinemachineFollow.FollowOffset.x, y, _cinemachineFollow.FollowOffset.z),
                duration, ease);
        }
        
        private void SetCameraYPosition(float y, float duration, Ease ease = Ease.Unset)
        {
            if (_mainCamera == null) return;

            _mainCamera.transform.DOMoveY(y, duration)
                .SetEase(ease);
        }
        
        public void ResetCamera()
        {
            if (_mainCamera == null) return;

            _mainCamera.transform.rotation = Quaternion.Euler(_defaultCameraRotation);
            _cinemachineFollow.FollowOffset = _defaultFollowOffset;
        }
    }
}
