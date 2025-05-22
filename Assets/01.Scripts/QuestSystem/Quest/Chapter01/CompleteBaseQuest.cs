using System;
using UnityEngine;

namespace JMT
{
    public class CompleteBaseQuest : QuestBase
    {
        private void Start()
        {
            tiles[0].OnBuild += HandleBuildEvent;
        }

        private void OnDestroy()
        {
            if (tiles == null) return;
            tiles[0].OnBuild -= HandleBuildEvent;
            tiles[0].CurrentBuilding.OnCompleteEvent -= HandleRunQuest;
        }

        private void HandleBuildEvent()
        {
            tiles[0].CurrentBuilding.OnCompleteEvent += HandleRunQuest;
        }

        private void HandleRunQuest()
        {
            RunQuest(0);
        }

        public override void Enable()
        {
            tiles[0].QuestPing.SelectPingLocation(true);
            base.Enable();
        }
    }
}
