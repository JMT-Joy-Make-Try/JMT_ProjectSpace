using JMT.UISystem;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class FightQuest : QuestBase
    {
        [SerializeField] private WaveSystem waveSystem;
        public override void Enable()
        {
            base.Enable();
            GameUIManager.Instance.TimeCompo.StartNightTime();
            waveSystem.OnClearEvent += HandleSpawnEvent;
        }

        private void HandleSpawnEvent()
        {
            RunQuest(0);
        }
    }
}
