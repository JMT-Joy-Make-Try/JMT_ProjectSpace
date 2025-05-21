using JMT.Planets.Tile;

namespace JMT.QuestSystem
{
    public class GettingQuest : QuestBase
    {
        private void Start()
        {
            tile.OnChangeInteraction += HandleChangeInteraction;
        }

        private void OnDestroy()
        {
            tile.OnChangeInteraction -= HandleChangeInteraction;
        }

        private void HandleChangeInteraction(TileInteraction interaction)
        {
            if(interaction.GetType() == typeof(NoneInteraction))
                RunQuest();
        }
    }
}
