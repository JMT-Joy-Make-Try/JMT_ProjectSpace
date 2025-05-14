using JMT.Core.Manager;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Events
{
    public class FuelEvent : Event
    {
        [SerializeField] private float fuelPercent = 20f;
        public override void StartEvent()
        {
            GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("연료 소모량 감소");
            Debug.Log("FuelEvent");
            BuildingManager.Instance.SetFuelDecreaseValuePercent(fuelPercent);
        }

        public override void EndEvent()
        {
            BuildingManager.Instance.ResetFuel();
        }
    }
}