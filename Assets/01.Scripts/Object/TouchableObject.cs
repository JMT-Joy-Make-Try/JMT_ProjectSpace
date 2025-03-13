using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.Object
{
    public class TouchableObject : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClick;
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}