using System;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class MakeBaseQuest : QuestBase
    {
        private void Start()
        {
            tile.OnBuild += HandleBuildEvent;
        }

        private void OnDestroy()
        {
            tile.OnBuild -= HandleBuildEvent;
            tile.CurrentBuilding.OnCompleteEvent -= RunQuest;
        }

        private void HandleBuildEvent()
        {
            tile.CurrentBuilding.OnCompleteEvent += RunQuest;
        }

        public override void Enable()
        {
            tile.QuestPing.SelectPingLocation(true);
            base.Enable();
        }
    }
}
