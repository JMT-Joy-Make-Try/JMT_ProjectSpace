using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class FadeUI : MonoBehaviour
    {
        [Tooltip("페이드 인 : 화면에 꺼매진게 사라졌을 때")]
        public event Action OnFadeInEvent;
        [Tooltip("페이드 아웃 : 화면이 꺼매질 때")]
        public event Action OnFadeOutEvent;

        [SerializeField] private Image fadeImage;

        private int fadeInValue = 0, fadeOutValue = 1;
        private float duration = 0.5f;

        public void OnFade(bool isFadeIn)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(fadeImage.DOFade(isFadeIn ? fadeInValue : fadeOutValue, duration));
            seq.OnComplete(() =>
            {
                if (isFadeIn) OnFadeInEvent?.Invoke();
                else OnFadeOutEvent?.Invoke();
            });
        }
    }
}
