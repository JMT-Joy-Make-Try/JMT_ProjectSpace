using UnityEngine;

namespace JMT
{
    public class CompleteBaseQuest : QuestBase
    {
        private void Start()
        {
            tile.OnBuild += HandleBuildEvent;
        }

        private void OnDestroy()
        {
            if (tile == null) return;
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
