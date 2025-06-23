namespace JMT.QuestSystem
{
    public class CreateBaseQuest : QuestBase
    {
        private void Start()
        {
            tiles[0].OnPrebuild += HandleRunQuest;
        }

        private void OnDestroy()
        {
            if (tiles == null) return;
            tiles[0].OnPrebuild -= HandleRunQuest;
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
