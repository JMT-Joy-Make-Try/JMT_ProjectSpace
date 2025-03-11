using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    [DefaultExecutionOrder(-10)]
    public class BuildCellUI : MonoBehaviour
    {
        private TextMeshProUGUI nameText;
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
