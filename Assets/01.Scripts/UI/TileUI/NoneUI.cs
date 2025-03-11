using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class NoneUI : PanelUI
    {
        private Button buildButton;

        private void Awake()
        {
            buildButton = PanelTrm.Find("BuildBtn").GetComponent<Button>();
            buildButton.onClick.AddListener(HandleBuildButton);
        }
        private void HandleBuildButton()
        {
            Debug.Log("Click Build Button");
            // 건물 짓기
        }
    }
}
