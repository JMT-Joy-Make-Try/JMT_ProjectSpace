using UnityEngine;
using UnityEngine.UIElements;

namespace JMT.UISystem
{
    public class PanelUI : MonoBehaviour
    {
        [SerializeField] private bool isFirstOn = false;
        protected VisualElement root;

        public void SetRoot(VisualElement root)
        {
            this.root = root;
            if (isFirstOn) OpenUI();
            else CloseUI();
        }

        public void OpenUI()
        {
            root.style.opacity = 1f;
        }

        public void CloseUI()
        {
            root.style.opacity = 0f;
        }
    }
}
