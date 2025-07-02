using JMT.CameraSystem;
using JMT.DayTime;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.UISystem.Result
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField] private ResultView view;
        [SerializeField] private ResultCountView countView;
        [SerializeField] private CameraEventSO eventSO;

        private bool isStartResult;
        private bool isEndResult;

        private void Awake()
        {
            eventSO.AddListener(HandleDaytimeEvent);
            GameUIManager.Instance.FadeCompo.OnFadeInEvent += HandleFadeInEvent;
            GameUIManager.Instance.FadeCompo.OnFadeOutEvent += HandleFadeOutEvent;
            view.OnNextButtonEvent += HandleNextButton;
        }

        private void OnDestroy()
        {
            eventSO.RemoveListener(HandleDaytimeEvent);
            GameUIManager.Instance.FadeCompo.OnFadeInEvent -= HandleFadeInEvent;
            GameUIManager.Instance.FadeCompo.OnFadeOutEvent -= HandleFadeOutEvent;
            view.OnNextButtonEvent -= HandleNextButton;
        }

        public void StartResult()
        {
            isStartResult = true;
            GameUIManager.Instance.FadeCompo.OnFade(false);
        }


        public void OpenPanel()
        {
            GameUIManager.Instance.GameUICompo.ClosePanel();
            GameUIManager.Instance.PlayerControlActive(false);

            view.OpenUI();
        }

        public void ClosePanel()
        {
            GameUIManager.Instance.GameUICompo.OpenPanel();
            GameUIManager.Instance.PlayerControlActive(true);
            GameUIManager.Instance.TimeCompo.StartDayTime();
            view.CloseUI();
        }

        private void HandleDaytimeEvent()
        {
            Debug.Log("결과띠");
            StartResult();
        }

        private void HandleFadeInEvent()
        {
            if(isStartResult)
            {
                isStartResult = false;
                countView.SetDay(GameUIManager.Instance.TimeCompo.DayCount);
            }
            else if(isEndResult)
            {
                isEndResult = false;
            }
        }

        private void HandleFadeOutEvent()
        {
            if(isStartResult)
            {
                StartCoroutine(FadeDelayRoutine());
                OpenPanel();
            }
            if(isEndResult)
            {
                StartCoroutine(FadeDelayRoutine());
                ClosePanel();
            }
        }

        private void HandleNextButton()
        {
            isEndResult = true;
            GameUIManager.Instance.FadeCompo.OnFade(false);
        }

        private IEnumerator FadeDelayRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            GameUIManager.Instance.FadeCompo.OnFade(true);
        }
    }
}
