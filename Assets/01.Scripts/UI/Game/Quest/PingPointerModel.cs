using System;
using System.Collections;
using UnityEngine;

namespace JMT
{
    public class PingPointerModel
    {
        public event Action OnOpenEvent;
        public event Action OnCloseEvent;

        private Coroutine delayRoutine;
        private bool isClose;

        public Quaternion GetRotation(Transform tileTrm, Transform playerTrm)
        {
            Vector3 tileScreenPos = Camera.main.WorldToScreenPoint(tileTrm.position);
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(playerTrm.position);
            Vector3 viewPos = Camera.main.WorldToViewportPoint(tileTrm.position);

            Vector2 dir = (tileScreenPos - playerScreenPos).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            if (IsVisible(viewPos) && !isClose)
            {
                OnCloseEvent?.Invoke();
                isClose = true;
            }
            else if (!IsVisible(viewPos) && isClose)
            {
                OnOpenEvent?.Invoke();
                isClose = false;
            }

            return rotation;
        }

        private bool IsVisible(Vector3 viewPos)
            => viewPos.z > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;
    }
}
