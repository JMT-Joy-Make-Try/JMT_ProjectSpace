using JMT.Building;
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
        [SerializeField] private Image select;
        public Button CellButton { get; private set; }

        private void Awake()
        {
            CellButton = GetComponent<Button>();
            SetSelect(false);
        }

        public void SetCell(ItemSO itemSO = null, string count = null)
        {
            if (itemSO != null)
            {
                if (icon != null)
                {
                    if (itemSO.ItemData.Icon != null)
                        icon.sprite = itemSO.ItemData.Icon;
                }
            }
            if(nameText != null) nameText.text = itemSO.ItemName;
            if (countText != null) countText.text = count;
        }
        
        public void SetCell(BuildingDataSO data = null)
        {
            if (icon != null)
            {
                if (data.Icon != null)
                    icon.sprite = data.Icon;
            }
            if (nameText != null) nameText.text = data.BuildingName;
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
            if (icon != null) icon.sprite = null;
            if (nameText != null) nameText.text = "";
            if (countText != null) countText.text = "";
        }

        public void SetSelect(bool isActive)
        {
            if(select != null)
            select.enabled = isActive;
        }
    }
}
