using System;
using System.Collections.Generic;

namespace JMT.Agent
{
    [Serializable]
    public class StatModifier
    {
        public StatModifierType ModifierType;
        public float Value;

        public StatModifier(StatModifierType modifierType, float value)
        {
            ModifierType = modifierType;
            Value = value;
        }
    }

    [Serializable]
    public class NPCStatData : StatData<NPCStatType>
    {
        public NPCStatData(NPCStatType type, float defaultValue) : base(type, defaultValue)
        {
        }
    }
}