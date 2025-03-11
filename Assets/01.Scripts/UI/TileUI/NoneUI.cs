using UnityEngine;
using UnityEngine.UIElements;

namespace JMT.UISystem
{
    public class NoneUI : PanelUI
    {
        private void OnEnable()
        {
            Button buildButton = root.Q<Button>("BuildBtn");
            buildButton.RegisterCallback<ClickEvent>(HandleBuildButton);
        }

        private void HandleBuildButton(ClickEvent evt)
        {
            Debug.Log("Click Build Button");
            // °Ç¹° Áþ±â
        }
    }
}
