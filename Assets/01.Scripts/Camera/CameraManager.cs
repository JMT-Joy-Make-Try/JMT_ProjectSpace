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
        [field: SerializeField] public CameraShaker CameraShakerCompo { get; private set; }
        [field: SerializeField] public CameraChanger CameraChangerCompo { get; private set; }
        [field: SerializeField] public CameraTransform CameraTransformCompo { get; private set; }
        [SerializeField] private CameraEventSO _cameraEventSO;
        
        public CinemachineCamera MainCamera => _mainCamera;

        protected override void Awake()
        {
            base.Awake();
            if (_mainCamera == null)
            {
                Debug.LogError("Main Camera is not assigned in CameraManager.");
                return;
            }
            CameraShakerCompo = GetComponent<CameraShaker>();
            CameraChangerCompo = GetComponent<CameraChanger>();
            CameraTransformCompo = GetComponent<CameraTransform>();

            CameraTransformCompo?.Init(_mainCamera);
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
            if (obj == DaytimeType.Night)
            {
                _mainCamera.DOZoom(16f, 1f);
                _cameraEventSO.Invoke();
            }
            else if (obj == DaytimeType.Day)
            {
                _mainCamera.DOZoom(14f, 1f);
            }
        }
    }
}
