using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public enum SpeedType
    {
        OneSpeed = 1,
        TwoSpeed = 2,
        ThreeSpeed = 3,
    }
    public class GameSpeedUI : MonoBehaviour
    {
        [SerializeField] private Button speedButton;
        private TextMeshProUGUI speedText;
        private SpeedType speedType = SpeedType.OneSpeed;

        private void Awake()
        {
            speedText = speedButton.GetComponentInChildren<TextMeshProUGUI>();

            speedButton.onClick.AddListener(HandleSpeedButton);
            ChangeSpeed(speedType);
        }

        private void HandleSpeedButton()
        {
            switch (speedType)
            {
                case SpeedType.OneSpeed:
                    speedType = SpeedType.TwoSpeed;
                    ChangeSpeed(speedType);
                    break;
                case SpeedType.TwoSpeed:
                    speedType = SpeedType.ThreeSpeed;
                    ChangeSpeed(speedType);
                    break;
                case SpeedType.ThreeSpeed:
                    speedType = SpeedType.OneSpeed;
                    ChangeSpeed(speedType);
                    break;
            }
        }

        private void ChangeSpeed(SpeedType speedType)
        {
            Time.timeScale = (int)speedType;
            speedText.text = (int)speedType + "x";
        }
    }
}
