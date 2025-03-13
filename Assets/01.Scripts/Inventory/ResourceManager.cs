using System;
using UnityEngine;

namespace JMT.Resource
{
    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        public event Action<int, int> OnFuelValueChanged;
        public event Action<int, int> OnOxygenValueChanged;

        [SerializeField] private int maxFuelValue, maxOxygenValue;
        [SerializeField] private int currentFuelValue, currentOxygenValue;

        public void AddFuel(int increaseValue)
        {
            currentFuelValue += increaseValue;
            OnFuelValueChanged?.Invoke(currentFuelValue, maxFuelValue);
        }

        public void AddOxygen(int increaseValue)
        {
            maxOxygenValue += increaseValue;
            OnOxygenValueChanged?.Invoke(currentOxygenValue, maxOxygenValue);
        }
    }
}
