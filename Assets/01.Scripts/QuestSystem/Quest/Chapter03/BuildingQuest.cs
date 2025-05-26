using UnityEngine;

namespace JMT.QuestSystem
{
    public class BuildingQuest : QuestBase
    {
        private void Start()
        {
            tiles[0].OnBuild += HandleBuildEvent;
        }

        private void OnDestroy()
        {
            if (tiles[0] == null) return;
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
    }
}
