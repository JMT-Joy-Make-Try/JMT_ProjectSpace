using JMT.Building.Component;
using System;

namespace JMT.QuestSystem
{
    public class PeopleQuest : QuestBase
    {
        private void Start()
        {
            tiles[0].OnBuild += HandleBuildEvent;
        }

        private void OnDestroy()
        {
            tiles[0].OnBuild -= HandleBuildEvent;
            if (tiles[0].CurrentBuilding != null) 
            tiles[0].CurrentBuilding.OnCompleteEvent -= HandleRunQuest;
        }

        private void HandleRunQuest()
        {
            RunQuest(0);
        }

        private void HandleBuildEvent()
        {
            tiles[0].CurrentBuilding.OnCompleteEvent += HandleRunQuest;
        }

        public override void Enable()
        {
            tiles[0].QuestPing.SelectPingLocation(false);
            base.Enable();
        }
    }
}
