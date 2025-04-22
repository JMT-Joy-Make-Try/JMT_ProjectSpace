using UnityEngine;

namespace JMT.UISystem.Popup
{
    public class PopupController : MonoBehaviour
    {
        [SerializeField] private PopupView view;

        public void SetActiveFixPopup(bool isActive, string str = null)
        {
            if(str != null)
                view.SetPopupText(str);
            view.ActiveFixPopup(isActive);
        }

        public void SetActiveAutoPopup(string str)
        {
            view.SetPopupText(str);
            view.ActiveAutoPopup();
        }
    }
}
