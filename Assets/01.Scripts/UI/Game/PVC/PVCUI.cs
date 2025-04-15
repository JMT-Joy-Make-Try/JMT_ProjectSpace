using System;
using System.Collections;
using UnityEngine;

namespace JMT
{
    public class PVCUI : MonoBehaviour
    {
        [SerializeField] private GameObject fill, complete;

        private void Awake()
        {
            ActiveUI(true, false);
        }

        public void SetTime(int time)
        {
            StartCoroutine(TimeRoutine(time));
        }

        private IEnumerator TimeRoutine(int time)
        {
            yield return new WaitForSeconds(time);
            ActiveUI(false, true);
        }

        public void ActiveUI(bool isFillActive, bool isCompleteActive)
        {
            fill.SetActive(isFillActive);
            complete.SetActive(isCompleteActive);
        }
    }
}
