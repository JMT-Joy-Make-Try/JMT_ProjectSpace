using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent
{
    public class StatData<T> : ISerializationCallbackReceiver where T : Enum
    {
        [field:SerializeField] public T Type { get; private set; }
        public float DefaultValue;
        public List<StatModifier> Modifiers;
        
        private Dictionary<StatModifierType, Func<float, float, float>> operations = new Dictionary<StatModifierType, Func<float, float, float>>
        {
            { StatModifierType.Addition, (a, b) => a + b },
            { StatModifierType.Subtraction, (a, b) => a - b },
            { StatModifierType.Multiplicative, (a, b) => a * b },
            { StatModifierType.Division, (a, b) => b != 0 ? a / b : throw new DivideByZeroException("Division by zero in stat modifier.") }
        };
        
        public StatData(T type, float defaultValue)
        {
            Type = type;
            DefaultValue = defaultValue;
            Modifiers = new List<StatModifier>();
        }
        
        public float GetValue()
        {
            float value = DefaultValue;
            foreach (var modifier in Modifiers)
            {
                Debug.Log(modifier.ModifierType.ToString());
                if (operations.TryGetValue(modifier.ModifierType, out var operation))
                {
                    value = operation(value, modifier.Value);
                }
                else
                {
                    throw new InvalidOperationException($"Unsupported modifier type: {modifier.ModifierType}");
                }
            }
            return value;
        }
        
        public void AddModifier(StatModifier modifier)
        {
            Modifiers.Add(modifier);
        }
        
        public void RemoveModifier(StatModifier modifier)
        {
            Modifiers.Remove(modifier);
        }

        public void OnBeforeSerialize()
        {
            operations = new Dictionary<StatModifierType, Func<float, float, float>>
            {
                { StatModifierType.Addition, (a, b) => a + b },
                { StatModifierType.Subtraction, (a, b) => a - b },
                { StatModifierType.Multiplicative, (a, b) => a * b },
                { StatModifierType.Division, (a, b) => b != 0 ? a / b : throw new DivideByZeroException("Division by zero in stat modifier.") },
                { StatModifierType.Percentage, (a, b) => a * (1 + b / 100f) } 
            };
        }

        public void OnAfterDeserialize()
        {
        }
    }
}