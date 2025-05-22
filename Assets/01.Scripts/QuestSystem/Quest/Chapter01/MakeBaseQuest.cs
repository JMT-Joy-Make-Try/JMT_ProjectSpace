using System;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class MakeBaseQuest : QuestBase
    {
        private void Start()
        {
            tiles[0].OnBuild += HandleRunQuest;
        }

        private void OnDestroy()
        {
            if (tiles == null) return;
            tiles[0].OnBuild -= HandleRunQuest;
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
