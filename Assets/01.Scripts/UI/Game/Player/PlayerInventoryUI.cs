using DG.Tweening;
using JMT.PlayerCharacter;
using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup playerInventoryGroup;
        [SerializeField] private Color redColor;
        [SerializeField] private TextMeshProUGUI playerInventoryValueText;
        [SerializeField] private PlayerInventory playerInventory;

        private void Awake()
        {
            playerInventory.OnInventoryEvent += HandleInventoryEvent;
            HandleInventoryEvent(0, 1);
        }

        private void OnDestroy()
        {
            playerInventory.OnInventoryEvent -= HandleInventoryEvent;
        }

        private void HandleInventoryEvent(int currentVal, int maxVal)
        {
            playerInventoryGroup.DOFade(currentVal == 0 ? 0 : 1f, 0.3f);
            playerInventoryValueText.DOColor(currentVal == maxVal ? redColor : Color.white, 0.3f);
            playerInventoryValueText.text = $"{currentVal}/{maxVal}";
        }
    }
}
