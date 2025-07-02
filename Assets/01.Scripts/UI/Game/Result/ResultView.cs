using JMT.NightSummary;
using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Result
{
    public class ResultView : PanelUI
    {
        public event Action OnNextButtonEvent;

        [Header("Data Contents")]
        [SerializeField] private CellUI rocketContent;
        [SerializeField] private CellUI itemContent;
        [SerializeField] private CellUI buildingContent;
        [SerializeField] private CellUI workerContent;
        [SerializeField] private CellUI reputationContent;

        [Header("Others")]
        [SerializeField] private Button nextButton;

        private void Awake()
        {
            nextButton.onClick.AddListener(HandleNextButton);
        }

        private void OnDestroy()
        {
            nextButton.onClick.RemoveListener(HandleNextButton);
        }

        public override void OpenUI()
        {
            var manager = NightSummaryManager.Instance;
            rocketContent.SetCell(null, manager.RocketStatusModule.PercentText);
            itemContent.SetCell(null, $"{manager.CollectItemModule.GetCollectedItemsCount()}개");
            buildingContent.SetCell(null, $"{manager.BuildingModule.GeBuildingsCount()}채");
            workerContent.SetCell(null, $"{manager.NPCCollectModule.GetCollectedNPCs().Count}명");
            reputationContent.SetCell(null, $"{manager.ReputationModule.CalculateReputation()}%");
            base.OpenUI();
        }

        private void HandleNextButton()
        {
            OnNextButtonEvent?.Invoke();
        }
    }
}
