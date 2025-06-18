using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class ItemCountUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Button minusButton, plusButton;

        public int Count { get; private set; } = 1;
        private int maxCount = 10;

        private void Awake()
        {
            minusButton.onClick.AddListener(HandleMinusButtonClick);
            plusButton.onClick.AddListener(HandlePlusButtonClick);
        }

        public void Init(int maxValue)
        {
            maxCount = maxValue;
            ItemCountText();
        }

        private void HandleMinusButtonClick()
        {
            SetItemCount(-1);
        }

        private void HandlePlusButtonClick()
        {
            SetItemCount(1);
        }

        private void SetItemCount(int value)
        {
            // 추가로 플레이어가 들 수 있는 최대치를 제한해야 합니다.
            Count = Mathf.Clamp(Count + value, 1, maxCount);
            ItemCountText(Count);
        }

        public void ItemCountText(int value = 1)
        {
            countText.text = value.ToString();
            Count = value;
        }
    }
}
