using DG.Tweening;

namespace JMT.UISystem
{
    public class SidePanelUI : PanelUI
    {
        public override void OpenUI()
        {
            base.OpenUI();
            PanelRectTrm.DOAnchorPosX(-50f, 0.3f).SetUpdate(true);
        }

        public override void CloseUI()
        {
            base.CloseUI();
            PanelRectTrm.DOAnchorPosX(PanelRectTrm.rect.width, 0.3f).SetUpdate(true);
        }
    }
}
