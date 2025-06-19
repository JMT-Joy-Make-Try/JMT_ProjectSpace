using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.UISystem
{
    public class TouchScreen : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnClickEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
        }
    }
}
