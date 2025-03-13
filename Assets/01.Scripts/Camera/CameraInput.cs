using JMT.InputSystem;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace JMT.CameraSystem
{
    public class CameraInput : MonoBehaviour
    {
        [SerializeField] private CameraInputSO inputSO;
        [SerializeField] private float camSpeed = 4f;
        [SerializeField] private float moveSpeed = 4f;
        private Coroutine zoomCoroutine, moveCoroutine;

        private void Awake()
        {
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


                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - deltaDistance * camSpeed, 50f, 1000f);

                prevDistance = distance;
                yield return null;
            }
        }
        private void HandleRotateStartEvent()
        {
            moveCoroutine = StartCoroutine(MoveCoroutine());
        }

        private void HandleRotateEndEvent()
        {
            StopCoroutine(moveCoroutine);
        }

        private IEnumerator MoveCoroutine()
        {
             float prevX = inputSO.GetPrimaryPosition().x;
             float prevY = inputSO.GetPrimaryPosition().y;
             Vector3 curPos = Camera.main.transform.localPosition;

             while (true)
             {
                 float deltaX = inputSO.GetPrimaryPosition().x - prevX;
                 float deltaY = inputSO.GetPrimaryPosition().y - prevY;

                 Camera.main.transform.localPosition += new Vector3(-(deltaX * moveSpeed), -(deltaY * moveSpeed));

                 prevX = inputSO.GetPrimaryPosition().x;
                 prevY = inputSO.GetPrimaryPosition().y;
                 yield return null;
             }
        }
    }
}
