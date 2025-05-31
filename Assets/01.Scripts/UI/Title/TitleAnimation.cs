using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Title
{
    public class TitleAnimation : MonoBehaviour
    {
        public event Action OnEndAnimationEvent;

        [SerializeField] private Image titleImage;
        [SerializeField] private float waitTime;
        [SerializeField] private float moveDuration;
        [SerializeField] private float fadeDuration;

        private Vector3 upScale = Vector3.one * 1.8f;
        private Vector3 originScale = Vector3.one * 1.3f;
        private Vector3 upPivot = new(0.5f, 1f);

        private Sequence seq;

        public bool IsEnd { get; private set; }

        public void StartAnimation()
        {
            titleImage.rectTransform.localScale = upScale;
            titleImage.DOFade(1f, fadeDuration);
            seq = DOTween.Sequence();
            seq.AppendInterval(waitTime);
            seq.Append(titleImage.rectTransform.DOPivot(upPivot, moveDuration));
            seq.Join(titleImage.rectTransform.DOAnchorMin(new(0.5f, 1f), moveDuration));
            seq.Join(titleImage.rectTransform.DOAnchorMax(new(0.5f, 1f), moveDuration));
            seq.Join(titleImage.rectTransform.DOAnchorPosY(-50f, moveDuration));
            seq.Join(titleImage.rectTransform.DOScale(originScale, moveDuration));
            seq.OnComplete(() =>
            {
                IsEnd = true;
                OnEndAnimationEvent?.Invoke();
            });
        }

        public void SkipAnimation()
        {
            seq?.Kill();
            titleImage.rectTransform.pivot = upPivot;
            titleImage.rectTransform.anchorMin = new(0.5f, 1f);
            titleImage.rectTransform.anchorMax = new(0.5f, 1f);
            titleImage.rectTransform.anchoredPosition = new(0, -50f);
            titleImage.rectTransform.localScale = originScale;
            IsEnd = true;
            OnEndAnimationEvent?.Invoke();
        }
    }
}
