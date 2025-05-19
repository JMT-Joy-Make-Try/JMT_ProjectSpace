using JMT.Item;
using JMT.UISystem;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public class ItemBuilding : BuildingBase
    {
        public ItemBuildingData data;
        public Queue<CellUI> ItemQueue { get; private set; } = new();

        protected override void Awake()
        {
            base.Awake();
            data.Init(this);
        }

        public void MakeItem(CreateItemSO createItemSO)
        {
            if (createItemSO.UseFuelCount > GameUIManager.Instance.ResourceCompo.CurrentFuelValue) return;
            if (data.Works.IsFull()) return;

            GameUIManager.Instance.ResourceCompo.AddFuel(-createItemSO.UseFuelCount);
            Debug.Log("작업을 시작합니다~!~! 대기열 리스트에 넣었습니당");
            BuildingWork work = new(createItemSO.ResultItem.ItemType, createItemSO.CreateTime);
            data.AddWork(work);
        }
        
        
    }
}