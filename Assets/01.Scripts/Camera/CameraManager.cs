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
        
        
        [Header("Extension")]
        [SerializeField] private CameraShaker _cameraShakerCompo;
        [SerializeField] private CameraChanger _cameraChangerCompo;
        [SerializeField] private CameraTransform _cameraTransformCompo;
        [SerializeField] private CameraEventSO _cameraEventSO;

        [Header("Property")]
        [SerializeField, Tooltip("Zoom Out을 얼마나 할 지 결정합니다.\n중요: 숫자가 커야 멀리 보임")] private float _zoomOutValue = 16f;
        [SerializeField] private float _zoomOutDuration = 5f;
        [SerializeField, Tooltip("Zoom In을 얼마나 할 지 결정합니다.\n중요: 숫자가 작아야 가까이 보임. (기본: 14)")] private float _zoomInValue = 14f;
        [SerializeField] private float _zoomInDuration = 1f;
        
        public CinemachineCamera MainCamera => _mainCamera;
        public CameraShaker CameraShakerCompo => _cameraShakerCompo;
        public CameraChanger CameraChangerCompo => _cameraChangerCompo;
        public CameraTransform CameraTransformCompo => _cameraTransformCompo;

        protected override void Awake()
        {
            base.Awake();
            if (_mainCamera == null)
            {
                Debug.LogError("Main Camera is not assigned in CameraManager.");
                return;
            }
            _cameraShakerCompo = GetComponent<CameraShaker>();
            _cameraChangerCompo = GetComponent<CameraChanger>();
            _cameraTransformCompo = GetComponent<CameraTransform>();

            _cameraTransformCompo?.Init(_mainCamera);
        }

        private void Start()
        {
            GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent += HandleNightEvent;
        }

        private void OnDestroy()
        {
            if (GameUIManager.HasInstance)
            {
                GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent -= HandleNightEvent;
            }
        }

        private void HandleNightEvent(DaytimeType obj)
        {
            Debug.Log("아아아아");
            if (obj == DaytimeType.Night)
            {
                _mainCamera.DOZoom(_zoomOutValue, _zoomOutDuration).OnComplete(() => _cameraEventSO.Invoke());
            }
            else if (obj == DaytimeType.Day)
            {
                _mainCamera.DOZoom(_zoomInValue, _zoomInDuration);
            }
        }
    }
}
