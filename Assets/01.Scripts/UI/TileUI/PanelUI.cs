using DG.Tweening;
using System;
using UnityEngine;

namespace JMT.UISystem
{
    public class PanelUI : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup panelGroup;
        [SerializeField] private bool isInteractable = true;
        [SerializeField] private bool isTimeStop = true;

        public Transform PanelTrm => panelGroup.transform;
        public Transform PanelRectTrm => PanelTrm as RectTransform;
        protected event Action OnCloseEvent;
        public virtual void OpenUI()
        {
            panelGroup.DOFade(1f, 0.3f).SetUpdate(true);
            if(isTimeStop)
                Time.timeScale = 0;

            if (!isInteractable) return;
            panelGroup.interactable = true;
            panelGroup.blocksRaycasts = true;
            //UIManager.Instance.NoTouchUI.ActiveNoTouchZone(true);
        }

        public virtual void CloseUI()
        {
            panelGroup.DOFade(0f, 0.3f).SetUpdate(true).OnComplete(() => OnCloseEvent?.Invoke());
            if(isTimeStop)
                Time.timeScale = SpeedSystem.Instance.TimeScale;

            if (!isInteractable) return;
            panelGroup.interactable = false;
            panelGroup.blocksRaycasts = false;
            //UIManager.Instance.NoTouchUI.ActiveNoTouchZone(false);
        }
    }
}
