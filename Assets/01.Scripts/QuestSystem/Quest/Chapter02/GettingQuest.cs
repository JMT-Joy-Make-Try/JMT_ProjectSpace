using JMT.Planets.Tile;

namespace JMT.QuestSystem
{
    public class GettingQuest : QuestBase
    {
        private void Start()
        {
            tiles[0].OnChangeInteraction += HandleChangeInteraction;
        }

        private void OnDestroy()
        {
            tiles[0].OnChangeInteraction -= HandleChangeInteraction;
        }

        private void HandleChangeInteraction(TileInteraction interaction)
        {
            if(interaction.GetType() == typeof(NoneInteraction))
                RunQuest(0);
        }
    }
}
