using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class ItemUI : PanelUI
    {
        private Button itemButton;

        private void Awake()
        {
            itemButton = PanelTrm.Find("ItemBtn").GetComponent<Button>();
            itemButton.onClick.AddListener(HandleMineButton);
        }

        private void HandleMineButton()
        {
            Debug.Log("Click Mine Button");
            // 건물 업그레이드
        }
    }
}
