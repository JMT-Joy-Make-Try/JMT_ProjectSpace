using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    [DefaultExecutionOrder(-10)]
    public class ItemCellUI : MonoBehaviour
    {
        private TextMeshProUGUI nameText, countText;
        private void Awake()
        {
            nameText = transform.Find("NameTxt").GetComponent<TextMeshProUGUI>();
            countText = transform.Find("CountTxt").GetComponent<TextMeshProUGUI>();
            SetItemCell(string.Empty, 0);
        }

        public void SetItemCell(string name, int count)
        {
            nameText.text = name;
            countText.text = count.ToString();
            // 사진도 넣어야함
        }
    }
}
