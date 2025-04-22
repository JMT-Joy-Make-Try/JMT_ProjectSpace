using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Building
{
    public class ItemBuildingView : PanelUI
    {
        public event Action OnExitButtonClicked;
        public event Action OnCreateButtonClicked;
        public event Action OnWorkerButtonClicked;
        public event Action OnUpgradeButtonClicked;

        [Header("Button")]
        [SerializeField] private Button exitButton;
        [SerializeField] private Button createButton;
        [SerializeField] private Button workerButton;
        [SerializeField] private Button upgradeButton;


        private void Awake()
        {
            exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());
            createButton.onClick.AddListener(() => OnCreateButtonClicked?.Invoke());
            workerButton.onClick.AddListener(() => OnWorkerButtonClicked?.Invoke());
            upgradeButton.onClick.AddListener(() => OnUpgradeButtonClicked?.Invoke());
        }

        private void OnDestroy()
        {
            exitButton.onClick.RemoveAllListeners();
            createButton.onClick.RemoveAllListeners();
            workerButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.RemoveAllListeners();
        }
    }
}
