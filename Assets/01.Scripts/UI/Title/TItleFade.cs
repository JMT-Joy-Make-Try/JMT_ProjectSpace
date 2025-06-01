using DG.Tweening;
using UnityEngine;

namespace JMT
{
    public class TItleFade : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private float duration = 1f;


        public void StartAnimation()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(group.DOFade(1f, duration));
            seq.AppendInterval(0.5f);

            seq.AppendCallback(() =>
            {
                Sequence blink = DOTween.Sequence();
                blink.Append(group.DOFade(0.3f, duration));
                blink.Append(group.DOFade(1f, duration));
                blink.AppendInterval(0.5f);
                blink.SetLoops(-1);
            });
        }
    }
}
