using JMT.Building.Component;

namespace JMT.QuestSystem
{
    public class PeopleQuest : QuestBase
    {
        private void Start()
        {
            tile.OnBuild += HandleBuildEvent;
        }

        private void OnDestroy()
        {
            tile.OnBuild -= HandleBuildEvent;

            if (tile.CurrentBuilding == null) return;
            BuildingNPC npc = tile.CurrentBuilding.GetBuildingComponent<BuildingNPC>();
            npc.OnChangeNpcEvent -= RunQuest;
        }

        private void HandleBuildEvent()
        {
            BuildingNPC npc = tile.CurrentBuilding.GetBuildingComponent<BuildingNPC>();
            npc.OnChangeNpcEvent += RunQuest;
        }

        public override void Enable()
        {
            tile.QuestPing.SelectPingLocation(false);
            base.Enable();
        }
    }
}
