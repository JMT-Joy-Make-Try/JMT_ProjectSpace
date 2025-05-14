using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    [Serializable]
    public struct AnimationColor
    {
        public Color firstColor, secondColor;

        public readonly void ChangeColor(Image image, bool isFirst, float duration)
        {
            Color changeColor = isFirst ? firstColor : secondColor;
            image.DOColor(changeColor, duration).SetUpdate(true);
        }
    }

    public class PanelUI : MonoBehaviour
    {
        protected event Action OnCloseEvent;

        [SerializeField] protected CanvasGroup panelGroup;
        [SerializeField] private bool isInteractable = true;
        [SerializeField] private bool isTimeStop = true;

        public Transform PanelTrm => panelGroup.transform;
        public RectTransform PanelRectTrm => PanelTrm as RectTransform;
        public bool IsOpen { get; private set; } = false;
        public virtual void OpenUI()
        {
            IsOpen = true;
            panelGroup.DOFade(1f, 0.3f).SetUpdate(true);
            if(isTimeStop)
                Time.timeScale = 0;

            panelGroup.blocksRaycasts = true;
            if (!isInteractable) return;
            panelGroup.interactable = true;
        }

        public virtual void CloseUI()
        {
            IsOpen = false;
            panelGroup.DOFade(0f, 0.3f).SetUpdate(true).OnComplete(() => OnCloseEvent?.Invoke());
            if(isTimeStop)
                Time.timeScale = GameUIManager.Instance.SpeedCompo.TimeScale;

            panelGroup.blocksRaycasts = false;
            if (!isInteractable) return;
            panelGroup.interactable = false;
        }
    }
}
