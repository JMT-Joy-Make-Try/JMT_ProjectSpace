using UnityEngine;
using UnityEngine.UIElements;

namespace JMT.UISystem
{
    public class PanelUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup panelRectTrm;
        [SerializeField] private bool isFirstOn = false;

        protected virtual void Awake()
        {
            if(isFirstOn) OpenUI();
            else CloseUI();
            // けいしぞ
        }
        
        public void OpenUI()
        {
        }

        public void CloseUI()
        {
        }
    }
}
