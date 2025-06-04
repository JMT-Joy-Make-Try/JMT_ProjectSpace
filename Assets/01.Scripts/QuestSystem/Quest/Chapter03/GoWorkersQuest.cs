using JMT.Building;
using JMT.Building.Component;
using JMT.Item;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace JMT.QuestSystem
{
    public class GoWorkersQuest : QuestBase
    {
        [SerializeField] private ItemSO item;
        private GatheringBuilding building;
        private int itemCount;

        private void Start()
        {
            tiles[0].OnBuild += HandleBuildEvent;
        }

        private void OnDestroy()
        {
            if (tiles == null || tiles[0] == null || tiles[0].CurrentBuilding == null) return;
            tiles[0].OnBuild -= HandleBuildEvent;
            tiles[0].CurrentBuilding.OnCompleteEvent -= HandleBuildCompleteEvent;
            if (building == null) return;
            building.OnAddItemEvent -= HandleAddItemQueueEvent;
        }

        private void HandleBuildEvent()
        {
            tiles[0].CurrentBuilding.OnCompleteEvent += HandleBuildCompleteEvent;
        }

        private void HandleBuildCompleteEvent()
        {
            building = tiles[0].CurrentBuilding as GatheringBuilding;
            building.OnAddItemEvent += HandleAddItemQueueEvent;
        }

        private void HandleAddItemQueueEvent()
        {
            Debug.Log("아이템이 큐에 저장되었습니다.");
            itemCount++;
            if (itemCount >= 5)
                RunQuest(0);
        }
        
        public override void Enable()
        {
            tiles[0].QuestPing.SelectPingLocation(true);
            base.Enable();
        }
    }
}
