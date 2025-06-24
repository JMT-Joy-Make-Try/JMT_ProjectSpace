namespace JMT.QuestSystem
{
    public class CreateBaseQuest : QuestBase
    {
        private void Start()
        {
            Tiles[0].OnPrebuild += HandleRunQuest;
        }

        private void OnDestroy()
        {
            if (Tiles == null) return;
            Tiles[0].OnPrebuild -= HandleRunQuest;
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
