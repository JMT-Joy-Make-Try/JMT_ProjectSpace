using System;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class MakeBaseQuest : QuestBase
    {
        private void Start()
        {
            tile.OnBuild += RunQuest;
        }

        private void OnDestroy()
        {
            if (tile == null) return;
            tile.OnBuild -= RunQuest;
            //tile.CurrentBuilding.OnCompleteEvent -= RunQuest;
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
