using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class BuildCellUI : MonoBehaviour
    {
        private Image image;
        private TextMeshProUGUI nameText;

        private void Start()
        {
        }
        private void Awake()
        {
            nameText = transform.Find("Name").GetComponentInChildren<TextMeshProUGUI>();
            image = transform.Find("Icon").GetComponent<Image>();
        }

        public void SetItemCell(string name)
        {
            nameText.text = name;
            // 사진도 넣어야함
        }
    }
}
