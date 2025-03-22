using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JMT.CameraSystem
{
    public class CameraInput : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO inputSO;
        [SerializeField] private float camSpeed = 4f;

        private Transform camRotateTrm;
        private Coroutine moveCoroutine;

        private void Awake()
        {
            camRotateTrm = transform.Find("Camera");
            inputSO.OnRotateStartEvent += HandleRotateStartEvent;
            inputSO.OnRotateEndEvent += HandleRotateEndEvent;
            inputSO.OnLookEvent += HandleLookEvent;
        }

        private void HandleRotateStartEvent()
        {
            if (inputSO.IsJoystickActive && Touchscreen.current.touches.Count <= 1)
            {
                return; // 조이스틱 사용 중에는 첫 번째 터치만 있을 경우 회전 안 함
            }
            moveCoroutine = StartCoroutine(RotateCoroutine());
        }

        private void HandleRotateEndEvent()
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
        }

        private IEnumerator RotateCoroutine()
        {
            while (true)
            {
                yield return null;
            }
        }

        private void HandleLookEvent(Vector2 delta)
        {
            if (inputSO.IsJoystickActive) return;

            Vector3 currentRotation = camRotateTrm.eulerAngles;
            currentRotation.y += delta.x * camSpeed * Time.deltaTime;
            camRotateTrm.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
