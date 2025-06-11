using UnityEngine;

namespace JMT.UISystem
{
    public abstract class BuildingController : MonoBehaviour
    {
        protected PanelUI currentPanel = null;

        public abstract void OpenPanel();
        public abstract void ClosePanel();

        public virtual void SetCurrentPanel(PanelUI panel)
        {
            if (currentPanel == panel) return;
            currentPanel?.CloseUI();
            currentPanel = panel;
            currentPanel?.OpenUI();
        }
    }
}
