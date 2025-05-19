using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JMT
{
    public class CategoryAnimation : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private List<Button> categoryButtons;
        [SerializeField] private float duration = 0.3f;

        [Header("Change Color")]
        [SerializeField] private bool isChangeColor;
        [SerializeField] private Color selectColor;
        [SerializeField] private Color notSelectColor;

        [Header("Change Size")]
        [SerializeField] private bool isChangeScale;
        [SerializeField] private float selectScale;
        [SerializeField] private float notSelectScale;

        [Header("Change Position")]
        [SerializeField] private bool isChangePos;
        [SerializeField] private Vector2 selectPos;
        [SerializeField] private Vector2 notSelectPos;

        private int currentButtonValue = -1;

        private void Awake()
        {
            for(int i = 0; i < categoryButtons.Count; ++i)
            {
                int value = i;
                categoryButtons[value].onClick.AddListener(() => HandleCategoryButton(value));
            }
            HandleCategoryButton(0);
        }

        private void HandleCategoryButton(int value)
        {
            if (currentButtonValue == value) return;
            currentButtonValue = value;

            for (int i = 0; i < categoryButtons.Count; ++i)
                SetAnimation(categoryButtons[i], value == i);
        }

        private void SetAnimation(Button button, bool isSelect)
        {
            if (isChangeColor)
                button.image.DOColor(isSelect ? selectColor : notSelectColor, duration);

            if (isChangeScale)
                button.image.rectTransform.DOScale(isSelect ? selectScale : notSelectScale, duration);

            if (isChangePos)
                button.image.rectTransform.DOAnchorPos(isSelect ? selectPos : notSelectPos, duration);
        }
    }
}
