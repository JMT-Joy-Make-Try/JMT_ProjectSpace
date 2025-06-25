using Unity.Cinemachine;
using UnityEngine;

namespace JMT.CameraSystem
{
    public class CameraChanger : MonoBehaviour
    {
        public Camera mainCamera;
    public float duration = 1.5f;
    public float orthoSize = 5f;
    public float perspectiveFOV = 60f;

    private bool isOrtho = false;
    private bool isTransitioning = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTransitioning)
        {
            isOrtho = !isOrtho;
            StartCoroutine(SmoothTransition(isOrtho));
        }
    }

    private System.Collections.IEnumerator SmoothTransition(bool toOrtho)
    {
        isTransitioning = true;

        float elapsed = 0f;

        float startFOV = mainCamera.fieldOfView;
        float targetFOV = toOrtho ? OrthoFOV(orthoSize) : perspectiveFOV;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;

        // 진짜로 바꿔주는 타이밍은 시각적 전환 후
        mainCamera.orthographic = toOrtho;
        isTransitioning = false;
    }

    /// <summary>
    /// Perspective FOV로 Ortho처럼 보이게 보정하는 함수
    /// </summary>
    private float OrthoFOV(float orthoSize)
    {
        float aspect = mainCamera.aspect;
        float distance = transform.position.z; // 카메라 거리 기준
        float frustumHeight = orthoSize * 2f;
        return 2.0f * Mathf.Atan(frustumHeight * 0.5f / distance) * Mathf.Rad2Deg;
    }
    }
}