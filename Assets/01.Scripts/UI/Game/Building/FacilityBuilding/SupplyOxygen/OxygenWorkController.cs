using JMT.Building;
using JMT.Planets.Tile;
using System;
using UnityEngine;

namespace JMT.UISystem.SupplyOxygen
{
    public class OxygenWorkController : MonoBehaviour
    {
        [SerializeField] private OxygenWorkView workView;
        [SerializeField] private OxygenWorkSideView sideView;

        private void Awake()
        {
            workView.OnOpenSideViewEvent += sideView.OpenUI;
            workView.OnCreateItemEvent += HandleCreateItemEvent;
            sideView.OnSelectItemEvent += workView.SetSelectItemPanel;
        }

        private void HandleCreateItemEvent(CreateItemSO item)
        {
            var workBuilding = TileManager.Instance.CurrentTile.CurrentBuilding as OxygenBuilding;

            if (item.UseFuelCount > GameUIManager.Instance.ResourceCompo.CurrentFuelValue) return;
            if (workBuilding.data.Works.IsFull()) return;

            GameUIManager.Instance.ResourceCompo.AddFuel(-item.UseFuelCount);
            Debug.Log("작업을 시작합니다.");
            BuildingWork work = new(item.ResultItem.ItemType, item.CreateTime);
            workBuilding.data.AddWork(work);
        }
    }
}
