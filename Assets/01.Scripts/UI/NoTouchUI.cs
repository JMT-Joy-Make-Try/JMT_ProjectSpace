using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.UISystem
{
    public class NoTouchUI : MonoBehaviour
    {
        [SerializeField] private NoTouchZone noTouchZone;

        public NoTouchZone NoTouchZone => noTouchZone;
        public void ActiveNoTouchZone(bool isTrue) => noTouchZone.gameObject.SetActive(isTrue);


    }
}
