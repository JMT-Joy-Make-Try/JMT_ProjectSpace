using DG.Tweening;
using JMT.Core.Tool;
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace JMT.CameraSystem
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private CinemachineCamera _mainCamera;
        
        [SerializeField] private Transform Test;


        public void LookCamera(Transform target, float duration)
        {
            Debug.Log(target.position);
            _mainCamera.transform.DOMove(target.position, duration, false, false, true);
        }

        public void ZoomCamera(float zoomValue, float duration)
        {
            float currentOrthographicSize = _mainCamera.Lens.OrthographicSize;
            Debug.Log(currentOrthographicSize);
            _mainCamera.DOZoom(currentOrthographicSize * (zoomValue / 10), duration);
        }
    }
}
