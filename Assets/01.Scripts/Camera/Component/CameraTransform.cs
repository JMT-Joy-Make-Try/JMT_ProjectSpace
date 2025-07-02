using DG.Tweening;
using JMT.Core.Tool;
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace JMT.CameraSystem
{
    public class CameraTransform : MonoBehaviour
    {
        private Vector3 _defaultCameraPosition;
        private Vector3 _defaultCameraRotation;
        private Vector3 _defaultFollowOffset;

        private CinemachineFollow _cinemachineFollow;

        private CinemachineCamera _mainCamera;

        public void Init(CinemachineCamera mainCamera)
        {
            _mainCamera = mainCamera ?? throw new ArgumentNullException(nameof(mainCamera), "Main Camera cannot be null.");
            _cinemachineFollow = _mainCamera.GetComponent<CinemachineFollow>();
            _defaultCameraRotation = _mainCamera.transform.rotation.eulerAngles;
            _defaultFollowOffset = _cinemachineFollow.FollowOffset;
        }

        public void RotationCamera(Vector3 rotation, float duration, Ease ease = Ease.Unset)
        {
            if (_mainCamera == null) return;

            _mainCamera.transform.DORotate(rotation, duration)
                .SetEase(ease);
        }

        
        
        private void SetCameraYPosition(float y, float duration, Ease ease = Ease.Unset)
        {
            if (_mainCamera == null) return;

            _mainCamera.transform.DOMoveY(y, duration)
                .SetEase(ease);
        }

        private void SetFollowOffset(float y, float duration, Ease ease = Ease.Unset)
        {
            if (_cinemachineFollow == null) return;
            _cinemachineFollow.DOFollowOffset(
                new Vector3(_cinemachineFollow.FollowOffset.x, y, _cinemachineFollow.FollowOffset.z),
                duration, ease);
        }

        public void ResetCamera()
        {
            if (_mainCamera == null) return;

            _mainCamera.transform.rotation = Quaternion.Euler(_defaultCameraRotation);
            _cinemachineFollow.FollowOffset = _defaultFollowOffset;
        }
    }
}