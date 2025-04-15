using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class ItemCellUI : MonoBehaviour
    {
        private Image icon;
        private TextMeshProUGUI nameText, countText;
        private void Awake()
        {
            icon = transform.Find("Icon").GetComponent<Image>();
            nameText = transform.Find("NameTxt").GetComponent<TextMeshProUGUI>();
            countText = transform.Find("CountTxt").GetComponent<TextMeshProUGUI>();
        }

        public virtual void SetItemCell(string name, int count, Sprite image)
        {
            if(image != null)
                icon.sprite = image;
            nameText.text = name;
            countText.text = count.ToString();
            // 사진도 넣어야함
        }

        public void ResetItemCell()
        {
            icon.sprite = null;
            nameText.text = "";
            countText.text = "";
        }
    }
}
