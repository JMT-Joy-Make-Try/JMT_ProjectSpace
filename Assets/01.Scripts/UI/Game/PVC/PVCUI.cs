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
            fill.SetActive(true);
            complete.SetActive(false);
        }

        public void SetTime(int time)
        {
            StartCoroutine(TimeRoutine(time));
        }

        private IEnumerator TimeRoutine(int time)
        {
            yield return new WaitForSeconds(time);
            fill.SetActive(false);
            complete.SetActive(true);
        }
    }
}
