using DG.Tweening;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace JMT.UISystem
{
    public class PanelUI : MonoBehaviour
    {
        protected event Action OnCloseEvent;

        [SerializeField] protected CanvasGroup panelGroup;
        [SerializeField] private bool isInteractable = true;
        [SerializeField] private bool isTimeStop = true;

        public Transform PanelTrm => panelGroup.transform;
        public Transform PanelRectTrm => PanelTrm as RectTransform;
        public bool IsOpen { get; private set; } = false;
        public virtual void OpenUI()
        {
            IsOpen = true;
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
            IsOpen = false;
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
