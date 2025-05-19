using DG.Tweening;
using UnityEngine;

namespace JMT.UISystem.Laboratory
{
    public class LaboratoryBottomView : PanelUI
    {
        public override void OpenUI()
        {
            PanelRectTrm.DOAnchorPosY(0f, 0.3f).SetUpdate(true);
        }

        public override void CloseUI()
        {
            PanelRectTrm.DOAnchorPosY(-250f, 0.3f).SetUpdate(true);
        }
    }
}
