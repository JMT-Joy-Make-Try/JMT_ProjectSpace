using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class CellUI : MonoBehaviour
    {
        public Action OnClickCellEvent;

        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText, countText;
        [SerializeField] private Image select;
        [SerializeField] private Button cellButton;

        public bool IsSelect { get; private set; }

        private void Awake()
        {
            cellButton?.onClick.AddListener(HandleCellButton);
            SetSelect(false);
        }

        private void OnDestroy()
        {
            cellButton?.onClick.RemoveListener(HandleCellButton);
        }

        private void HandleCellButton()
        {
            OnClickCellEvent?.Invoke();
        }

        public void SetCell(ICellDisplayData data = null, string count = null)
        {
            if (data != null)
            {
                if (icon != null) icon.sprite = data.DisplayIcon;
                if (nameText != null) nameText.text = data.DisplayName;
            }

            if (countText != null) countText.text = count;
        }

        public void ResetCell()
        {
            if (icon != null) icon.sprite = null;
            if (nameText != null) nameText.text = "";
            if (countText != null) countText.text = "";
        }

        public bool ChangeSelect()
        {
            IsSelect = !IsSelect;
            SetSelect(IsSelect);
            return IsSelect;
        }

        public void SetSelect(bool isActive)
        {
            IsSelect = isActive;
            if (select != null)
                select.enabled = isActive;
        }
    }
}
