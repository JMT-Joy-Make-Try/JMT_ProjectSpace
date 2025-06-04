using System.Collections.Generic;
using UnityEngine;

namespace JMT.DataSystem
{
    public class NPCSpriteSystem : MonoSingleton<NPCSpriteSystem>
    {
        // 0번 = 건강 좋음
        // 1번 = 건강 중간
        // 2번 = 건강 나쁨

        [SerializeField] private List<Sprite> healthIcons;
        [SerializeField] private List<Sprite> npcIcons;

        public Sprite GetHealthIcon(int index) 
            => healthIcons[index];

        public Sprite GetNpcIcon(int index)
            => npcIcons[index];
    }
}
