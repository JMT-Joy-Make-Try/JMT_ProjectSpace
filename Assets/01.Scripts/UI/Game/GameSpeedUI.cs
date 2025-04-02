using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    
    public class GameSpeedUI : MonoBehaviour
    {
        [SerializeField] private Button speedButton;
        private TextMeshProUGUI speedText;

        private void Awake()
        {
            speedText = speedButton.GetComponentInChildren<TextMeshProUGUI>();

            ChangeSpeedText(SpeedType.OneSpeed);
            speedButton.onClick.AddListener(() => SpeedSystem.Instance.ChangeSpeed());
            SpeedSystem.Instance.OnSpeedChangeEvent += ChangeSpeedText;
        }

        private void ChangeSpeedText(SpeedType speedType)
        {
            speedText.text = (int)speedType + "x";
        }
    }
}
