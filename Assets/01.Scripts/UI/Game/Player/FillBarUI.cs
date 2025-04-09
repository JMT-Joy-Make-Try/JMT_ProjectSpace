using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class FillBarUI : WorldCanvasUI
    {
        [SerializeField] private Image fill;

        public void SetHpBar(int curValue, int maxValue)
        {
            fill.DOFillAmount(curValue / (float)maxValue, 0.2f).SetEase(Ease.Linear);
        }

        public void SetHpBar(int curValue, int maxValue, float time)
        {
            fill.DOFillAmount(curValue / (float)maxValue, time).SetEase(Ease.Linear);
        }

        public void ResetBar(float value)
        {
            fill.fillAmount = value;
        }
    }
}
