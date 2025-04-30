using DG.Tweening;
using TMPro;
using UnityEngine;

namespace JMT.UISystem.Popup
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup panelGroup;
        [SerializeField] private TextMeshProUGUI popupText;
        private Sequence seq;

        public void ActiveFixPopup(bool isActive)
        {
            panelGroup.DOFade(isActive ? 1 : 0, 0.3f).SetUpdate(true);
        }

        public void ActiveAutoPopup()
        {
            panelGroup.alpha = 0f;
            seq?.Kill();
            seq = DOTween.Sequence();
            seq.Append(panelGroup.DOFade(1, 0.3f)).SetUpdate(true);
            seq.AppendInterval(0.5f);
            seq.Append(panelGroup.DOFade(0, 0.3f)).SetUpdate(true);
        }

        public void SetPopupText(string str)
        {
            popupText.text = str;
        }
    }
}
