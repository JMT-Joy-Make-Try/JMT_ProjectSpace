using UnityEngine;
using UnityEngine.EventSystems;

public class LookTouchHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public System.Action<float> OnLookDelta;

    private int activePointerId = -1;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.position.x < Screen.width * 0.5f)
            return;

        activePointerId = eventData.pointerId;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != activePointerId)
            return;

        float deltaX = eventData.delta.x;
        OnLookDelta?.Invoke(deltaX);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == activePointerId)
            activePointerId = -1;
    }
}