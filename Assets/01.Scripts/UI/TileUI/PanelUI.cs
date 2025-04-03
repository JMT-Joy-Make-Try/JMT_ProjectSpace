using DG.Tweening;
using UnityEngine;

namespace JMT.UISystem
{
    public class PanelUI : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup panelGroup;

        public Transform PanelTrm => panelGroup.transform;
        public Transform PanelRectTrm => PanelTrm as RectTransform;

        public virtual void OpenUI()
        {
            panelGroup.DOFade(1f, 0.3f).SetUpdate(true);
            panelGroup.interactable = true;
            panelGroup.blocksRaycasts = true;
            Time.timeScale = 0;
            //UIManager.Instance.NoTouchUI.ActiveNoTouchZone(true);
        }

        public virtual void CloseUI()
        {
            panelGroup.DOFade(0f, 0.3f).SetUpdate(true);
            panelGroup.interactable = false;
            panelGroup.blocksRaycasts = false;
            Debug.Log(SpeedSystem.Instance.TimeScale);
            Time.timeScale = SpeedSystem.Instance.TimeScale;
            //UIManager.Instance.NoTouchUI.ActiveNoTouchZone(false);
        }
    }
}
