using UnityEngine;
using UnityEngine.UIElements;

namespace JMT.UISystem
{
    public class ItemUI : PanelUI
    {
        private void OnEnable()
        {
            Button mineButton = root.Q<Button>("MineBtn");
            mineButton.RegisterCallback<ClickEvent>(HandleMineButton);
        }

        private void HandleMineButton(ClickEvent evt)
        {
            Debug.Log("Click Upgrade Button");
            // 건물 업그레이드
        }
    }
}
