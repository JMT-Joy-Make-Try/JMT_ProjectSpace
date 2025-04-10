using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    public class BuildCellUI : MonoBehaviour
    {
        private TextMeshProUGUI nameText;

        private void Start()
        {
        }
        private void Awake()
        {
            nameText = transform.Find("NameTxt").GetComponent<TextMeshProUGUI>();
        }

        public void SetItemCell(string name)
        {
            nameText.text = name;
            // 사진도 넣어야함
        }
    }
}
