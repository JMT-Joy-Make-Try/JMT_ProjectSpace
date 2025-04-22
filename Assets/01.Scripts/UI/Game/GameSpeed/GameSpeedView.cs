using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JMT.UISystem.GameSpeed
{
    
    public class GameSpeedView : MonoBehaviour
    {
        [SerializeField] private Button speedButton;
        [SerializeField] private TextMeshProUGUI speedText;

        public void SetSpeedButtonListener(UnityAction action)
        {
            speedButton.onClick.AddListener(action);
        }

        public void ChangeSpeedText(SpeedType speedType)
        {
            speedText.text = (int)speedType + "x";
        }
    }
}
