using DG.Tweening;
using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    public class PopupUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup panelGroup;
        private TextMeshProUGUI popupText;
        private Sequence seq;

        private void Awake()
        {
            popupText = panelGroup.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ActiveInteractPopup(bool isActive)
        {
            panelGroup.DOFade(isActive ? 1 : 0, 0.3f).SetUpdate(true);
        }

        public void ActiveAutoPopup()
        {
            panelGroup.alpha = 0f;
            if(seq != null)
                seq.Kill();
            seq = DOTween.Sequence();
            seq.Append(panelGroup.DOFade(1, 0.3f)).SetUpdate(true);
            seq.AppendInterval(0.5f);
            seq.Append(panelGroup.DOFade(0, 0.3f)).SetUpdate(true);
        }

        public void SetPopupText(string str)
        {
            popupText.text = str;
            ActiveAutoPopup();
        }
    }
}
