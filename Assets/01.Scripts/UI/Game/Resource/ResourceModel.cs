using System;
using UnityEngine;

namespace JMT.UISystem.Resource
{
    public class ResourceModel
    {
        public event Action<int, int> OnFuelValueChanged;
        public event Action<int, int> OnNpcValueChanged;

        private int currentFuelValue, currentNpcValue;

        public int MaxFuelValue { get; private set; }
        public int MaxNpcValue { get; private set; }
        public int CurrentFuelValue => currentFuelValue;
        public int CurrentNpcValue => currentNpcValue;

        public ResourceModel(int maxFuel, int maxNpc)
        {
            MaxFuelValue = maxFuel;
            MaxNpcValue = maxNpc;
            currentFuelValue = maxFuel;
        }

        public void AddFuel(int increaseValue)
        {
            currentFuelValue += increaseValue;
            OnFuelValueChanged?.Invoke(currentFuelValue, MaxFuelValue);
        }

        public void AddNpc(int increaseValue)
        {
            currentNpcValue += increaseValue;
            OnNpcValueChanged?.Invoke(currentNpcValue, MaxNpcValue);
        }

        public void AddMaxNpc(int increaseValue)
        {
            MaxNpcValue += increaseValue;
            OnNpcValueChanged?.Invoke(currentNpcValue, MaxNpcValue);
        }
    }
}
