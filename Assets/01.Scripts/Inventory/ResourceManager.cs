using System;
using UnityEngine;

namespace JMT.Resource
{
    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        public event Action<int, int> OnFuelValueChanged;
        public event Action<int, int> OnOxygenValueChanged;

        [SerializeField] private int maxFuelValue, maxOxygenValue;
        private int currentFuelValue, currentOxygenValue;

        private void Start()
        {
            AddFuel(maxFuelValue);
            AddOxygen(maxOxygenValue);
        }

        public void AddFuel(int increaseValue)
        {
            currentFuelValue += increaseValue;
            OnFuelValueChanged?.Invoke(currentFuelValue, maxFuelValue);
        }

        public void AddOxygen(int increaseValue)
        {
            currentOxygenValue += increaseValue;
            OnOxygenValueChanged?.Invoke(currentOxygenValue, maxOxygenValue);
        }
    }
}
