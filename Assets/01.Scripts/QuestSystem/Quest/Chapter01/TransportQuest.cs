using JMT.Building.Component;

namespace JMT.QuestSystem
{
    public class TransportQuest : QuestBase
    {
        private void Start()
        {
            Tiles[0].OnBuild += HandleRunQuest;
        }

        private void OnDestroy()
        {
            Tiles[0].OnBuild -= HandleRunQuest;
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
