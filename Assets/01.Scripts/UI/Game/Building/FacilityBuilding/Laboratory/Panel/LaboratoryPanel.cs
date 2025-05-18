using DG.Tweening;
using UnityEngine;

namespace JMT.UISystem.Laboratory
{
    public class LaboratoryPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group;
        private float duration = 0.2f;

        private RectTransform rectTrm => group.transform as RectTransform;

        public void OpenUI()
        {
            rectTrm.DOAnchorPosX(-50f, duration).SetUpdate(true);
            group.DOFade(1f, duration).SetUpdate(true);
        }

        public void CloseUI()
        {
            rectTrm.DOAnchorPosX(rectTrm.rect.width, duration).SetUpdate(true);
            group.DOFade(0f, duration).SetUpdate(true);
        }
    }
}
