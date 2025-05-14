using UnityEngine;

namespace JMT.UISystem
{
    public class PingPointerView : PanelUI
    {
        public void SetRotation(Quaternion qu)
            => PanelRectTrm.rotation = qu;
    }
}
