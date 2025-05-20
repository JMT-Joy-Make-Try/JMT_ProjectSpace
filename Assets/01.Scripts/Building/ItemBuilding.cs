using JMT.UISystem;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public class ItemBuilding : BuildingBase
    {
        public ItemBuildingData data;
        public Queue<CreateItemSO> ItemQueue { get; private set; } = new();

        protected override void Awake()
        {
            base.Awake();
            data.Init(this);
        }

        public void MakeItem(CreateItemSO item)
        {
            if (item.UseFuelCount > GameUIManager.Instance.ResourceCompo.CurrentFuelValue) return;
            if (data.Works.IsFull()) return;

            GameUIManager.Instance.ResourceCompo.AddFuel(-item.UseFuelCount);
            Debug.Log("대기열 리스트에 작업을 추가했습니다.");
            ItemQueue.Enqueue(item);
            BuildingWork work = new(item.ResultItem.ItemType, item.CreateTime);
            data.AddWork(work);
        }
        
        
    }
}