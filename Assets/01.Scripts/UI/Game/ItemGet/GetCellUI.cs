using DG.Tweening;
using JMT.Item;
using TMPro;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class GetCellUI : PanelUI
    {
        private Image icon;
        private TextMeshProUGUI valueText;

        private void Awake()
        {
            icon = transform.Find("Icon").GetComponent<Image>();
            valueText = transform.Find("ValueTxt").GetComponent<TextMeshProUGUI>();
            OnCloseEvent += HandleCloseEvent;
        }

        public void SetData(ItemSO item, int count)
        {
            icon.sprite = item.Icon;
            valueText.text = item.ItemName + " + " + count;

            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() => OpenUI());
            seq.AppendInterval(1f);
            seq.AppendCallback(() => CloseUI());
        }


        private void HandleCloseEvent()
        {
            Destroy(gameObject);
        }
    }
}
