using JMT.Building;
using JMT.Building.Component;

namespace JMT.QuestSystem
{
    public class BuildSpaceshipQuest : QuestBase
    {
        private void Start()
        {
            Tiles[0].OnBuild += HandleBuildEvent;
        }

        private void OnDestroy()
        {
            Tiles[0].OnBuild -= HandleBuildEvent;
            if (Tiles[0].CurrentBuilding != null)
                Tiles[0].CurrentBuilding.GetBuildingComponent<BuildingBuilder>().PVC.OnGaugeFull -= HandleRunQuest;
        }

        private void HandleBuildEvent()
        {
            if(Tiles[0].CurrentBuilding is RocketLauncherBuilding)
                Tiles[0].CurrentBuilding.GetBuildingComponent<BuildingBuilder>().PVC.OnGaugeFull += HandleRunQuest;
        }

        private void HandleRunQuest()
        {
            RunQuest(0);
        }

        public override void Enable()
        {
            Tiles[0].QuestPing.SelectPingLocation(true);
            base.Enable();
        }
    }
}
