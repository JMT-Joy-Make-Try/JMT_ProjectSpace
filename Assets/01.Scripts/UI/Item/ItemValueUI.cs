using DG.Tweening;
using JMT.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class ItemValueUI : PanelUI
    {
        [SerializeField] private Color redColor;
        [SerializeField] private Image inventoryIcon;
        [SerializeField] private TextMeshProUGUI playerInventoryValueText;
        [SerializeField] private bool isRedColor = true;
        [SerializeField] private bool isGameObjectActive = false;

        public override void OpenUI()
        {
            base.OpenUI();
            if(isGameObjectActive) panelGroup.gameObject.SetActive(true);
        }

        public override void CloseUI()
        {
            base.CloseUI();
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(0.3f);
            seq.AppendCallback(() =>
            {
                if (isGameObjectActive)
                    panelGroup.gameObject.SetActive(false);
            });
        }

        public void SetItemCount(ItemSO item, int currentVal, int maxVal)
        {
            if(isRedColor) playerInventoryValueText.DOColor(currentVal == maxVal ? redColor : Color.white, 0.3f);
            playerInventoryValueText.text = $"{currentVal}/{maxVal}";

            if (item != null)
                inventoryIcon.sprite = item.DisplayIcon;
        }
    }
}
