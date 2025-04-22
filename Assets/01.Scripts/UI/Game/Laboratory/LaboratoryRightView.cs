using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Laboratory
{
    public class LaboratoryRightView : MonoBehaviour
    {
        public Action OnItemCreateEvent;
        [SerializeField] private RectTransform infoPanel, upgradePanel;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Button createButton;

        private void Awake()
        {
            createButton.onClick.AddListener(() => OnItemCreateEvent?.Invoke());
        }

        private void OnDestroy()
        {
            createButton.onClick.RemoveAllListeners();
        }

        public void OpenInfoPanel()
        {
            infoPanel.DOAnchorPosX(-50f, 0.3f).SetUpdate(true);
            upgradePanel.DOAnchorPosX(upgradePanel.rect.width, 0.3f).SetUpdate(true);
        }

        public void OpenUpgradePanel()
        {
            upgradePanel.DOAnchorPosX(-50f, 0.3f).SetUpdate(true);
            infoPanel.DOAnchorPosX(infoPanel.rect.width, 0.3f).SetUpdate(true);
        }
    }
}
