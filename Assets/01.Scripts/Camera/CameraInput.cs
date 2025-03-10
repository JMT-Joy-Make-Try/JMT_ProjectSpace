using JMT.InputSystem;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace JMT.CameraSystem
{
    public class CameraInput : MonoBehaviour
    {
        [SerializeField] private float camSpeed = 4f;
        [SerializeField] private float rotateSpeed = 4f;
        [SerializeField] private CameraInputSO inputSO;
        [SerializeField] private Transform camParentTrm;
        private Transform camTransform;
        private Coroutine zoomCoroutine;
        private Coroutine rotateCoroutine;

        private void Awake()
        {
            camTransform = camParentTrm.GetChild(0);
            inputSO.OnZoomStartEvent += HandleZoomStartEvent;
            inputSO.OnZoomEndEvent += HandleZoomEndEvent;
            inputSO.OnRotateStartEvent += HandleRotateStartEvent;
            inputSO.OnRotateEndEvent += HandleRotateEndEvent;
        }

        private void HandleZoomStartEvent()
        {
            zoomCoroutine = StartCoroutine(ZoomDetection());
        }

        private void HandleZoomEndEvent()
        {
            StopCoroutine(zoomCoroutine);
        }

        private IEnumerator ZoomDetection()
        {
            float prevDistance = Vector2.Distance(inputSO.GetPrimaryPosition(), inputSO.GetSecondaryPosition());
            while (true)
            {
                float distance = Vector2.Distance(inputSO.GetPrimaryPosition(), inputSO.GetSecondaryPosition());
                float deltaDistance = distance - prevDistance;

                Vector3 targetPos = camTransform.localPosition;
                targetPos.z -= deltaDistance * camSpeed;
                targetPos.z = Mathf.Clamp(targetPos.z, 0.3f, 3f);

                camTransform.localPosition = new Vector3(camTransform.localPosition.x, camTransform.localPosition.y,
                                            Mathf.Lerp(camTransform.localPosition.z, targetPos.z, Time.deltaTime * camSpeed));

                prevDistance = distance;
                yield return null;
            }
        }
        private void HandleRotateStartEvent()
        {
            rotateCoroutine = StartCoroutine(RotateCoroutine());
        }

        private void HandleRotateEndEvent()
        {
            StopCoroutine(rotateCoroutine);
        }

        private IEnumerator RotateCoroutine()
        {
            float prevX = inputSO.GetPrimaryPosition().x;
            float prevY = inputSO.GetPrimaryPosition().y;
            Vector3 currentRotation = camParentTrm.eulerAngles;

            while (true)
            {
                float currentX = inputSO.GetPrimaryPosition().x;
                float xValue = prevX - currentX;
                float currentY = inputSO.GetPrimaryPosition().y;
                float yValue = prevY - currentY;

                currentRotation.y -= xValue * Time.deltaTime * rotateSpeed;
                currentRotation.x -= yValue * Time.deltaTime * rotateSpeed;

                if (currentRotation.x > 180f) currentRotation.x -= 360f;
                currentRotation.x = Mathf.Clamp(currentRotation.x, -60f, 60f);

                camParentTrm.rotation = Quaternion.Euler(currentRotation);

                prevX = currentX;
                prevY = currentY;
                yield return null;
            }
        }
    }
}
