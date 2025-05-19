using JMT.Building;
using JMT.Item;
using JMT.PlayerCharacter;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Laboratory
{
    public class LaboratoryController : MonoBehaviour
    {
        [SerializeField] private List<BuildingDataSO> lockBuildingList = new();
        [SerializeField] private List<ToolSO> lockItemList = new();

        [SerializeField] private LaboratoryView view;
        [SerializeField] private LaboratoryBottomView bottomView;
        [SerializeField] private StudyView studyView;
        [SerializeField] private ToolView toolView;
        [SerializeField] private UpgradeView upgradeView;

        public List<BuildingDataSO> LockBuildingList => lockBuildingList;
        public List<ToolSO> LockItemList => lockItemList;

        private LaboratoryCategory currentCategory;

        private PanelUI currentUI;
        private BuildingDataSO currentBuildingData;
        private ToolSO currentItemData;

        private void Awake()
        {
            view.OnChangedCategory += HandleChangedCategory;
            view.OnExitEvent += CloseUI;
            studyView.OnStudyButtonEvent += HandleItemCreateEvent;
        }

        private void OnDestroy()
        {
            view.OnChangedCategory -= HandleChangedCategory;
            view.OnExitEvent -= CloseUI;
            studyView.OnStudyButtonEvent -= HandleItemCreateEvent;
        }

        public void OpenUI()
        {
            view.OpenUI();
            ChangeCurrentUI(studyView);
        }
        public void CloseUI() => view.CloseUI();

        private void HandleChangedCategory(LaboratoryCategory category)
        {
            if (currentCategory == category) return;
            currentCategory = category;

            view.ResetCell();
            if(currentCategory != LaboratoryCategory.Upgrade)
            {
                bottomView.OpenUI();
                //buildingInfoView.OpenUI();
            }
            currentBuildingData = null;
            currentItemData = null;

            switch (category)
            {
                case LaboratoryCategory.Study:
                    view.SetCell(LockBuildingList, HandleSetInfo);
                    ChangeCurrentUI(studyView);
                    break;
                case LaboratoryCategory.Equipment:
                    view.SetCell(LockItemList, HandleSetInfo);
                    ChangeCurrentUI(toolView);
                    break;
                case LaboratoryCategory.Worker:
                    Debug.Log("일꾼 관리 창입니다.");
                    //view.SetCell(LockItemList, HandleSetInfo);

                    break;
                case LaboratoryCategory.Upgrade:
                    bottomView.CloseUI();
                    ChangeCurrentUI(upgradeView);
                    break;
            }
        }

        private void ChangeCurrentUI(PanelUI panel)
        {
            currentUI?.CloseUI();
            currentUI = panel;
            currentUI?.OpenUI();
        }

        private void HandleItemCreateEvent()
        {
            Debug.Log("버튼을 눌렀습니다.");
            bool isCalculate = GameUIManager.Instance.InventoryCompo.CalculateItem(currentItemData.NeedItems);
            if(isCalculate)
            {
                GameUIManager.Instance.InventoryCompo.AddItem(currentItemData, 1);
                Debug.Log("아이템이 들어왔습니다!");
            }
        }

        private void HandleSetInfo(BuildingDataSO data)
        {
            currentBuildingData = data;
            // studyView.SetInfo();
        }

        private void HandleSetInfo(ToolSO data)
        {
            currentItemData = data;
            toolView.SetInfo(data);
        }
    }
}
