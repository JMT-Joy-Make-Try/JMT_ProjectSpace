using DG.Tweening;
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace JMT.CameraSystem
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private CinemachineCamera _mainCamera;
        
        [SerializeField] private Transform Test;

        public void LookAt(Transform target)
        {
            _mainCamera.LookAt = target;
            CameraMove(target.position, 10);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LookAt(Test);
            }
        }

        public void CameraMove(Vector3 position)
        {
            _mainCamera.transform.position = position;
        }

        public void CameraMove(Vector3 position, float duration)
        {
            _mainCamera.transform.DOMove(position, duration);
        }
    }
}
