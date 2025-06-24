using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.NightSummary;
using JMT.Planet.Tile;
using JMT.Planets.Tile;
using JMT.Sound;
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
        [SerializeField] private bool _isOnceBuild = false;
        [SerializeField] private BuildingDataSO buildingDataSO;
        #region Building Component
        public List<IBuildingComponent> components = new List<IBuildingComponent>();
        
        private Dictionary<Type, IBuildingComponent> _componentLookup = new Dictionary<Type, IBuildingComponent>();
        #endregion
        
        protected virtual void Awake()
        {
            InitBuildingComponents();
        }

        protected virtual void Start()
        {
            BuildingManager.Instance.AddBuilding(this);
            AddEvents();
            var buildingLevel = GetBuildingComponent<BuildingLevel>();
            NightSummaryManager.Instance?.BuildingModule.AddBuilding(buildingDataSO.BuildingType, buildingLevel.CurLevel, 1);
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
            GetBuildingComponent<BuildingBuilder>().OnBuildingEvent += HandleBuildingEnd;
        }

        

        protected virtual void RemoveEvents()
        {
            GetBuildingComponent<BuildingFuel>().OnFuelEmptyEvent -= HandleFuelEmpty;
            GetBuildingComponent<BuildingWorker>().OnWorkingEvent -= HandleWorkingEvent;
            GetBuildingComponent<BuildingBuilder>().OnGaugeFullEvent -= HandleGaugeFull;
            GetBuildingComponent<BuildingBuilder>().OnBuildingEvent -= HandleBuildingEnd;
        }

        private void HandleBuildingEnd()
        {
            var interaction = GetPlanetTile().ChangeInteraction<ProgressInteraction>();
            interaction.Interaction();
            if (_isOnceBuild)
                BuildingManager.Instance.GetDictionary().Remove(buildingDataSO);
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
            Debug.Log(gameObject.name);
            GameUIManager.Instance.InteractCompo.StopInfinityHold();
            GetBuildingComponent<BuildingBuilder>().BuildBuilding();
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