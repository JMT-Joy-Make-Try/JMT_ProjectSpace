using JMT.UISystem;

namespace JMT.QuestSystem
{
    public class CheckSpaceshipQuest : QuestBase
    {
        private void Start()
        {
            BuildingUIManager.Instance.RocketCompo.OnPanelEvent += HandleRunQuest;
        }

        private void OnDestroy()
        {
            BuildingUIManager.Instance.RocketCompo.OnPanelEvent -= HandleRunQuest;
        }

        private void HandleRunQuest(bool isOpen)
        {
            if(isOpen)
                RunQuest(0);
        }

        public override void Enable()
        {
            Tiles[0].QuestPing.SelectPingLocation(true);
            base.Enable();
        }
    }
}
