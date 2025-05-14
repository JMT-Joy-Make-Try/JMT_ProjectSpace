using JMT.Core.Manager;
using UnityEngine;

namespace JMT.Planets.Events
{
    public class FuelEvent : Event
    {
        [SerializeField] private float fuelPercent = 20f;
        public override void StartEvent()
        {
            Debug.Log("FuelEvent");
            BuildingManager.Instance.SetFuelDecreaseValuePercent(fuelPercent);
        }

        public override void EndEvent()
        {
            BuildingManager.Instance.ResetFuel();
        }
    }
}