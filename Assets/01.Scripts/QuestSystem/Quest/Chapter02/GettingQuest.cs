using JMT.Planets.Tile;
using System;

namespace JMT.QuestSystem
{
    public class GettingQuest : QuestBase
    {
        private Action<TileInteraction>[] handlers;

        private void Start()
        {
            handlers = new Action<TileInteraction>[tiles.Count];

            for (int i = 0; i < tiles.Count; i++)
            {
                int tileIndex = i;

                // 이름 없는 함수(람다)로 쓰게 되면 OnDestroy에서 문제가 발생할 수도 있음.
                // 따라서 수행할 함수를 변수로 저장해서 핸들러를 등록함.
                handlers[tileIndex] = (interaction) => HandleChangeInteraction(interaction, tileIndex);
                tiles[tileIndex].OnChangeInteraction += handlers[tileIndex];
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                int tileIndex = i;
                tiles[tileIndex].OnChangeInteraction -= handlers[tileIndex];
            }
        }

        private void HandleChangeInteraction(TileInteraction interaction, int index)
        {
            if(interaction.GetType() == typeof(NoneInteraction))
                RunQuest(index);
        }
    }
}
