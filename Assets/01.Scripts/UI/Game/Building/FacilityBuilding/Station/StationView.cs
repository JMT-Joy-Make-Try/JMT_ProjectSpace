using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class StationView : PanelUI
    {
        public event Action OnExitButtonEvent;

        [SerializeField] private Button exitButton;

        private void Awake()
        {
            exitButton.onClick.AddListener(() => OnExitButtonEvent?.Invoke());
        }

        private void OnDestroy()
        {
            exitButton.onClick.RemoveAllListeners();
        }
    }
}
