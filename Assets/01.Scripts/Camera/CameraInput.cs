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
            //camTransform = camParentTrm.GetChild(0);
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
                Debug.Log(deltaDistance);

                Vector3 targetPos = camTransform.localPosition;
                targetPos.y -= deltaDistance * camSpeed;
                targetPos.y = Mathf.Clamp(targetPos.z, 110f, 400f);

                camParentTrm.localPosition = new Vector3(camParentTrm.localPosition.x, Mathf.Lerp(camParentTrm.localPosition.y, targetPos.y, Time.deltaTime * camSpeed));

                prevDistance = distance;
                yield return null;
            }
        }
        private void HandleRotateStartEvent()
        {
            rotateCoroutine = StartCoroutine(MoveCoroutine());
        }

        private void HandleRotateEndEvent()
        {
            StopCoroutine(rotateCoroutine);
        }

        private IEnumerator MoveCoroutine()
        {
            float prevX = inputSO.GetPrimaryPosition().x;
            float prevY = inputSO.GetPrimaryPosition().y;
            Vector3 curPos = camTransform.localPosition;

            while (true)
            {
                float deltaX = inputSO.GetPrimaryPosition().x - prevX;
                float deltaY = inputSO.GetPrimaryPosition().y - prevY;

                camTransform.localPosition += new Vector3(-(deltaX * rotateSpeed), 0, -(deltaY * rotateSpeed));

                prevX = inputSO.GetPrimaryPosition().x;
                prevY = inputSO.GetPrimaryPosition().y;
                yield return null;
            }
            
        }
    }
}
