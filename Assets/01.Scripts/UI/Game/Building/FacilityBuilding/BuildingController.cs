using UnityEngine;

namespace JMT.UISystem
{
    public abstract class BuildingController : MonoBehaviour
    {
        private PanelUI currentPanel = null;

        public abstract void OpenPanel();
        public abstract void ClosePanel();

        public void SetCurrentPanel(PanelUI panel)
        {
            if (currentPanel == panel) return;
            currentPanel?.CloseUI();
            currentPanel = panel;
            currentPanel?.OpenUI();
        }
    }
}
