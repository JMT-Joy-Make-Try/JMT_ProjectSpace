using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.UISystem
{
    public class NoTouchUI : MonoBehaviour
    {
        [SerializeField] private TouchScreen noTouchZone;

        public TouchScreen NoTouchZone => noTouchZone;
        public void ActiveNoTouchZone(bool isTrue) => noTouchZone.gameObject.SetActive(isTrue);


    }
}
