using JMT.Item;
using JMT.UISystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Building
{
    public class ItemBuilding : BuildingBase
    {
        public event Action<ItemSO> OnAddItemQueueEvent;

        public ItemBuildingData data;
        public Queue<CreateItemSO> ItemQueue { get; private set; } = new();

        public float GaugeValue { get; private set; }

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
            OnAddItemQueueEvent?.Invoke(item.ResultItem);
            ItemQueue.Enqueue(item);
            BuildingWork work = new(item.ResultItem.ItemType, item.CreateTime);
            data.AddWork(work);
        }

        public override void Work()
        {
            base.Work();
            StartCoroutine(WorkCoroutine());
            
        }

        private IEnumerator WorkCoroutine()
        {
            while (_isWorking)
            {
                GaugeValue = 0f;
                CreateItemSO item = data.GetFirstCreateItem();
                int itemCount = data.CreateItemList.Select(s => s.ResultItem.ItemType).Count();
                //npcAgent.WorkData.SetData(item, item.CreateTime, itemCount);
                if (item == null || data.CreateItemList.Count <= 0)
                {
                    Debug.Log("Building Data is null");
                    yield break;
                }

                int timeMinute = item.CreateTime.GetSecond();
                yield return StartCoroutine(Gauge(timeMinute));
                data.RemoveWork();
            }
        }

        private IEnumerator Gauge(int timeMinute)
        {
            float gaugeSpeed = 1f / timeMinute;

            while (GaugeValue < 1f)
            {
                GaugeValue += Time.deltaTime * gaugeSpeed;
                yield return null;
            }
            GaugeValue = 1f;
        }
    }
}