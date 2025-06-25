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

        // private void Start()
        // {
        //     GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent += HandleNightEvent;
        // }

        // private void OnDestroy()
        // {
        //     if (GameUIManager.Instance != null)
        //     {
        //         GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent -= HandleNightEvent;
        //     }
        // }

        private void HandleNightEvent(DaytimeType obj)
        {
            Sequence sequence = DOTween.Sequence();
            if (obj == DaytimeType.Night)
            {
                sequence.Append(_mainCamera.DOZoom(12f, 1f));
                sequence.AppendCallback(() => RotationCamera(new Vector3(-7.5f, 45, 0), 1f));
                sequence.AppendCallback(() => SetFollowOffset(26.9f, 1f));
            }
            else if (obj == DaytimeType.Day)
            {
                sequence.Append(_mainCamera.DOZoom(14f, 1f));
                sequence.AppendCallback(() => RotationCamera(new Vector3(-7.5f, 45, 0), 1f));
                sequence.AppendCallback(() => SetFollowOffset(12.6f, 1f));
            }
            
            sequence.Play();
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
