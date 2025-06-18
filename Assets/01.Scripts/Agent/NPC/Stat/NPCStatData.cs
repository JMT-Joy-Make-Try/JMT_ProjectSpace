using System;
using System.Collections.Generic;

namespace JMT.Agent
{
    [Serializable]
    public class NPCStatModifier
    {
        public NPCStatModifierType ModifierType;
        public float Value;

        public NPCStatModifier(NPCStatModifierType modifierType, float value)
        {
            ModifierType = modifierType;
            Value = value;
        }
    }

    [Serializable]
    public class NPCStatData
    {
        public NPCStatType Type;
        public float DefaultValue;
        public List<NPCStatModifier> Modifiers;
        
        public NPCStatData(NPCStatType type, float defaultValue)
        {
            Type = type;
            DefaultValue = defaultValue;
            Modifiers = new List<NPCStatModifier>();
        }
        
        public float GetValue()
        {
            float value = DefaultValue;
            foreach (var modifier in Modifiers)
            {
                if (modifier.ModifierType == NPCStatModifierType.Additive)
                {
                    value += modifier.Value;
                }
                else if (modifier.ModifierType == NPCStatModifierType.Multiplicative)
                {
                    value *= modifier.Value;
                }
            }
            return value;
        }
        
        public void AddModifier(NPCStatModifier modifier)
        {
            Modifiers.Add(modifier);
        }
        
        public void RemoveModifier(NPCStatModifier modifier)
        {
            Modifiers.Remove(modifier);
        }
    }
}