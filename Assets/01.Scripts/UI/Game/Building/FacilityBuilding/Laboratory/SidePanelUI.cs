using DG.Tweening;
using UnityEngine;

namespace JMT.UISystem
{
    public class SidePanelUI : PanelUI
    {
        [SerializeField] private float outRange = -50f;
        public override void OpenUI()
        {
            base.OpenUI();
            PanelRectTrm.DOAnchorPosX(outRange, 0.3f).SetUpdate(true);
        }

        public override void CloseUI()
        {
            base.CloseUI();
            PanelRectTrm.DOAnchorPosX(PanelRectTrm.rect.width, 0.3f).SetUpdate(true);
        }
    }
}
