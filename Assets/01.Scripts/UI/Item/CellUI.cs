using JMT.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class CellUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText, countText;

        public void SetCell(ItemSO itemSO = null, string count = null)
        {
            if(icon != null)
            {
                if (itemSO.Icon != null)
                    icon.sprite = itemSO.Icon;
            }
            if(nameText != null) nameText.text = name;
            if (countText != null) countText.text = count;
        }

        public void SetCell(string name = null, string count = null, Sprite icon = null)
        {
            if (this.icon != null) 
            {
                if (icon != null)
                    this.icon.sprite = icon;
            }
            if (nameText != null) nameText.text = name;
            if (countText != null) countText.text = count;
        }

        public void ResetCell()
        {
            icon.sprite = null;
            nameText.text = "";
            countText.text = "";
        }
    }
}
