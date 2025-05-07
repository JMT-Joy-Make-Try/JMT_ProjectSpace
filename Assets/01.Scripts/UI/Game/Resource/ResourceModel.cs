using System;
using UnityEngine;

namespace JMT.UISystem.Resource
{
    public class ResourceModel
    {
        public event Action<float, float> OnFuelValueChanged;
        public event Action<int, int> OnNpcValueChanged;

        private float currentFuelValue;
        private int currentNpcValue;

        public float MaxFuelValue { get; private set; }
        public int MaxNpcValue { get; private set; }
        public float CurrentFuelValue => currentFuelValue;
        public int CurrentNpcValue => currentNpcValue;

        public ResourceModel(float maxFuel, int maxNpc)
        {
            MaxFuelValue = maxFuel;
            MaxNpcValue = maxNpc;
            currentFuelValue = maxFuel;
        }

        public void AddFuel(float increaseValue)
        {
            currentFuelValue += increaseValue;
            if (currentFuelValue > MaxFuelValue)
            {
                currentFuelValue = MaxFuelValue;
            }
            else if (currentFuelValue < 0)
            {
                currentFuelValue = 0;
            }
            OnFuelValueChanged?.Invoke(currentFuelValue, MaxFuelValue);
        }

        public void AddNpc(int increaseValue)
        {
            currentNpcValue += increaseValue;
            OnNpcValueChanged?.Invoke(currentNpcValue, MaxNpcValue);
        }

        public void AddMaxNpc(int increaseValue)
        {
            MaxNpcValue = increaseValue;
            OnNpcValueChanged?.Invoke(currentNpcValue, MaxNpcValue);
        }
    }
}
