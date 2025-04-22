using JMT.Building;
using JMT.Item;
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
        [SerializeField] private LaboratoryRightView rightView;

        public List<BuildingDataSO> LockBuildingList => lockBuildingList;
        public List<ToolSO> LockItemList => lockItemList;

        private LaboratoryCategory currentCategory;

        private BuildingDataSO currentBuildingData;
        private ToolSO currentItemData;

        private void Awake()
        {
            view.OnChangedCategory += HandleChangedCategory;
            view.OnExitEvent += CloseUI;
            rightView.OnItemCreateEvent += HandleItemCreateEvent;
        }

        private void OnDestroy()
        {
            view.OnChangedCategory -= HandleChangedCategory;
            view.OnExitEvent -= CloseUI;
            rightView.OnItemCreateEvent -= HandleItemCreateEvent;
        }

        public void OpenUI() => view.OpenUI();
        public void CloseUI() => view.CloseUI();

        private void HandleChangedCategory(LaboratoryCategory category)
        {
            if (currentCategory == category) return;
            currentCategory = category;

            view.ResetCell();
            if(currentCategory != LaboratoryCategory.Upgrade)
            {
                bottomView.OpenUI();
                rightView.OpenInfoPanel();
            }
            currentBuildingData = null;
            currentItemData = null;

            switch (category)
            {
                case LaboratoryCategory.Study:
                    view.SetCell(LockBuildingList, HandleSetInfo);
                    break;
                case LaboratoryCategory.Equipment:
                    view.SetCell(LockItemList, HandleSetInfo);
                    break;
                case LaboratoryCategory.Worker:
                    Debug.Log("일꾼 관리 창입니다.");
                    break;
                case LaboratoryCategory.Upgrade:
                    bottomView.CloseUI();
                    rightView.OpenUpgradePanel();
                    break;
            }
        }

        private void HandleItemCreateEvent()
        {
            Debug.Log("버튼을 눌렀습니다.");
            bool isCalculate = GameUIManager.Instance.InventoryCompo.CalculateItem(currentItemData.NeedItems);
            if(isCalculate)
            {
                Debug.Log("아이템이 들어왔습니다!");
                GameUIManager.Instance.InventoryCompo.AddItem(currentItemData, 1);
            }
        }

        private void HandleSetInfo(BuildingDataSO data)
        {
            currentBuildingData = data;
        }

        private void HandleSetInfo(ToolSO data)
        {
            currentItemData = data;
        }
    }
}
