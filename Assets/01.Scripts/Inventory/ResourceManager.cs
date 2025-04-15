using System;
using UnityEngine;

namespace JMT.Resource
{
    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        public event Action<int, int> OnFuelValueChanged;
        public event Action<int, int> OnNpcValueChanged;


        [field: SerializeField] public int MaxFuelValue { get; private set; }
        [field: SerializeField] public int MaxNpcValue { get; private set; }

        public int CurrentFuelValue => currentFuelValue;
        public int CurrentNpcValue => currentNpcValue;

        private int currentFuelValue, currentNpcValue;

        private void Start()
        {
            AddFuel(MaxFuelValue);
            AddNpc(0);
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
