using JMT.Agent;
using JMT.Building.Component;
using JMT.Planets.Tile;
using JMT.UISystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Building
{
    public abstract class BuildingBase : MonoBehaviour
    {
        #region Building Component
        public List<IBuildingComponent> components = new List<IBuildingComponent>();
        
        private Dictionary<Type, IBuildingComponent> _componentLookup = new Dictionary<Type, IBuildingComponent>();
        #endregion
        
        public bool IsBuilding { get; private set; }
        public Action OnCompleteEvent;
        
        [SerializeField] private float _fuelAmount;
        
        protected bool _isWorking;
        private PVCBuilding _pvc;
        
        public PVCBuilding PVC => _pvc;
        
        protected virtual void Awake()
        {
            InitBuildingComponents();
            
            OnCompleteEvent += HandleCompleteEvent;
        }

        private IEnumerator FuelRoutine()
        {
            while (true)
            {
                if (GameUIManager.Instance.ResourceCompo.CurrentFuelValue <= 0)
                {
                    StopWork();
                    var npcList = GetBuildingComponent<BuildingNPC>();
                    npcList.RemoveAllNpc();
                    yield break;
                }
                GameUIManager.Instance.ResourceCompo.AddFuel(-_fuelAmount);
                yield return new WaitForSeconds(1f);
            }
        }

        private void OnDestroy()
        {
            OnCompleteEvent -= HandleCompleteEvent;
        }


        protected virtual void InitBuildingComponents()
        {
            components = GetComponents<IBuildingComponent>().ToList();
            foreach (var component in components)
            {
                component?.Init(this);
                _componentLookup.Add(component.GetType(), component);
            }
        }

        protected virtual void HandleCompleteEvent()
        {
            var visual = GetBuildingComponent<BuildingVisual>();
            visual.BuildingTransparent(1f);
            _pvc.PlayAnimation();
            visual.SetFloatProperty("_Alpha", 1f);
            StartCoroutine(FuelRoutine());
        }

        public void Building()
        {
            var visual = GetBuildingComponent<BuildingVisual>();
            visual.SetMaterial(visual.VisualMat);

            var buildingData = GetBuildingComponent<BuildingData>().Data;
            StartCoroutine(BuildingRoutine(buildingData.buildingLevel[0].BuildTime.GetSecond()));
        }

        private IEnumerator BuildingRoutine(int time)
        {
            GetBuildingComponent<BuildingVisual>().BuildingTransparent(0.3f);
            yield return new WaitForSeconds(time);
            IsBuilding = true;
        }

        public virtual void Work()
        {
            if (_isWorking)
            {
                return;
            }

            _isWorking = true;
            GetBuildingComponent<BuildingAnimator>().SetAnimation(_isWorking);
        }
        
        public virtual void StopWork()
        {
            if (!_isWorking)
            {
                return;
            }

            _isWorking = false;
            GetBuildingComponent<BuildingAnimator>().SetAnimation(_isWorking);
        }
        
        public void SetWorking(bool isWorking)
        {
            _isWorking = isWorking;
            GetBuildingComponent<BuildingAnimator>().SetAnimation(_isWorking);
        }
        
        protected PlanetTile GetPlanetTile()
        {
            return transform.parent.parent.GetComponent<PlanetTile>();
        }
        
        public void SetPVCBuilding(PVCBuilding pvc)
        {
            _pvc = pvc;
        }
        
        public T GetBuildingComponent<T>() where T : IBuildingComponent
        {
            if (_componentLookup.TryGetValue(typeof(T), out var component))
            {
                return (T)component;
            }

            foreach (var comp in components)
            {
                if (comp is T matchedComponent)
                {
                    return matchedComponent;
                }
            }

            Debug.LogError($"Component of type {typeof(T)} not found in {gameObject.name}");
            return default;
        }

    }
}