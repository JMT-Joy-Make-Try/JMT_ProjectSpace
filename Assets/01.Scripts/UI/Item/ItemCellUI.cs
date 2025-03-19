using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    public class ItemCellUI : MonoBehaviour
    {
        private TextMeshProUGUI nameText, countText;
        private void Awake()
        {
            nameText = transform.Find("NameTxt").GetComponent<TextMeshProUGUI>();
            countText = transform.Find("CountTxt").GetComponent<TextMeshProUGUI>();
        }

        public virtual void SetItemCell(string name, int count)
        {
            nameText.text = name;
            countText.text = count.ToString();
            // 사진도 넣어야함
        }
    }
}
