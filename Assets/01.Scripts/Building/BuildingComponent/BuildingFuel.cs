using JMT.UISystem;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingFuel : MonoBehaviour, IBuildingComponent
    {
        public BuildingBase Building { get; private set; }
        [SerializeField] private float _fuelAmount;
        public float FuelAmount
        {
            get => _fuelAmount;
            set => _fuelAmount = value;
        }
        
        public event Action OnFuelEmptyEvent;
        
        public void Init(BuildingBase building)
        {
            Building = building;
            building.GetBuildingComponent<BuildingBuilder>().OnCompleteEvent += StartFuelRoutine;
        }
        
        private void OnDestroy()
        {
            Building.GetBuildingComponent<BuildingBuilder>().OnCompleteEvent -= StartFuelRoutine;
        }

        private void StartFuelRoutine()
        {
            StartCoroutine(FuelRoutine());
        }

        private IEnumerator FuelRoutine()
        {
            while (true)
            {
                if (GameUIManager.Instance.ResourceCompo.CurrentFuelValue <= 0)
                {
                    OnFuelEmptyEvent?.Invoke();
                    
                    yield break;
                }
                GameUIManager.Instance.ResourceCompo.AddFuel(-_fuelAmount);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}