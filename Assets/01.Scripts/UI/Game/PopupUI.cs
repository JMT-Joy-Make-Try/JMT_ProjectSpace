using DG.Tweening;
using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    public class PopupUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup interactPopup;
        private TextMeshProUGUI interactText;

        private void Awake()
        {
            interactText = interactPopup.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ActiveInteractPopup(bool isActive)
        {
            interactPopup.DOFade(isActive ? 1 : 0, 0.3f);
        }

        public void SetInteractPopup(string str)
        {
            interactText.text = str;
        }
    }
}
