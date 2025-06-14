using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using JMT.Sound;
using System;
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
        
        protected virtual void Awake()
        {
            InitBuildingComponents();
            BuildingManager.Instance.AddBuilding(this);
        }
        
        protected virtual void Start()
        {
            AddEvents();
        }
        
        protected virtual void OnDestroy()
        {
            RemoveEvents();
        }

        protected virtual void AddEvents()
        {
            GetBuildingComponent<BuildingFuel>().OnFuelEmptyEvent += HandleFuelEmpty;
            GetBuildingComponent<BuildingWorker>().OnWorkingEvent += HandleWorkingEvent;
            GetBuildingComponent<BuildingBuilder>().OnGaugeFullEvent += HandleGaugeFull;
        }

        

        protected virtual void RemoveEvents()
        {
            GetBuildingComponent<BuildingFuel>().OnFuelEmptyEvent -= HandleFuelEmpty;
            GetBuildingComponent<BuildingWorker>().OnWorkingEvent -= HandleWorkingEvent;
            GetBuildingComponent<BuildingBuilder>().OnGaugeFullEvent -= HandleGaugeFull;
        }

        private void HandleWorkingEvent(bool isWorking)
        {
            GetBuildingComponent<BuildingAnimator>().SetAnimation(isWorking);
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

        private void HandleFuelEmpty()
        {
            GetBuildingComponent<BuildingWorker>().StopWork();
            var npcList = GetBuildingComponent<BuildingNPC>();
            npcList.RemoveAllNpc();
        }
        
        public PlanetTile GetPlanetTile()
        {
            return transform.parent.parent.GetComponent<PlanetTile>();
        }

        private void HandleGaugeFull()
        {
            GetPlanetTile().ChangeInteraction<ProgressInteraction>();
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