using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.GameSpeed
{
    public class GameSpeedView : MonoBehaviour
    {
        public event Action OnSpeedButtonEvent;

        [SerializeField] private Button speedButton;
        [SerializeField] private TextMeshProUGUI speedText;

        private void Awake()
        {
            speedButton.onClick.AddListener(HandleSpeedButtonEvent);
        }

        private void OnDestroy()
        {
            speedButton.onClick.RemoveListener(HandleSpeedButtonEvent);
        }

        private void HandleSpeedButtonEvent()
            => OnSpeedButtonEvent?.Invoke();

        public void ChangeSpeedText(SpeedType speedType)
        {
            speedText.text = (int)speedType + "x";
        }
    }
}
