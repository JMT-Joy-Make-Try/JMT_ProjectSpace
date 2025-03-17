using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.UISystem
{
    public class NoTouchZone : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
        }
    }
}
