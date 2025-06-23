using JMT.Agent;
using System;

namespace JMT.PlayerCharacter
{
    [Serializable]
    public class PlayerStatData : StatData<PlayerStatType>
    {
        public PlayerStatData(PlayerStatType type, float defaultValue) : base(type, defaultValue)
        {
        }
    }
}