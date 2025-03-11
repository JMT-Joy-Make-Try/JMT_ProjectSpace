using DG.Tweening;
using UnityEngine;

namespace JMT.UISystem
{
    public class PanelUI : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup panelGroup;

        public Transform PanelTrm => panelGroup.transform;
        public Transform PanelRectTrm => PanelTrm as RectTransform;

        public void OpenUI()
        {
            panelGroup.DOFade(1f, 0.3f);
            panelGroup.interactable = true;
            panelGroup.blocksRaycasts = true;
        }

        public void CloseUI()
        {
            panelGroup.DOFade(0f, 0.3f);
            panelGroup.interactable = false;
            panelGroup.blocksRaycasts = false;
        }
    }
}
