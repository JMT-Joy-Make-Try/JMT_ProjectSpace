using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace JMT.CameraSystem
{
    public class CameraChanger : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<CameraType, CinemachineCamera> cameras;

        private CinemachineCamera _currentCameraType;

        public void ChangeCamera(CameraType cameraType)
        {
            if (cameras.TryGetValue(cameraType, out var camera))
            {
                if (_currentCameraType != null)
                {
                    _currentCameraType.Priority = 0; // Reset current camera priority
                }

                camera.Priority = 1; // Set new camera priority
                _currentCameraType = camera;
            }
            else
            {
                Debug.LogWarning($"Camera of type {cameraType} not found.");
            }
        }
    }

    public enum CameraType
    {
        Game,
        NightSummary,
    }
}