using JMT.Building.Component;
using JMT.Planets.Tile;
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
        // public BuildingData BuildingData { get; private set; }
        // public BuildingAnimator BuildingAnimator { get; private set; }
        // public BuildingHealth BuildingHealth { get; private set; }
        // public BuildingVisual BuildingVisual { get; private set; }
        // public BuildingLevel BuildingLevel { get; private set; }
        // public BuildingNPC BuildingNPC { get; private set; }
        #endregion
        
        public bool IsBuilding { get; private set; }
        public Action OnCompleteEvent;
        
        protected bool _isWorking;
        private PVCBuilding _pvc;
        
        public PVCBuilding PVC => _pvc;
        
        protected virtual void Awake()
        {
            InitBuildingComponents();
            
            OnCompleteEvent += HandleCompleteEvent;
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
            }
            // BuildingVisual = GetComponent<BuildingVisual>();
            // BuildingAnimator = GetComponent<BuildingAnimator>();
            // BuildingNPC = GetComponent<BuildingNPC>();
            // BuildingHealth = GetComponent<BuildingHealth>();
            // BuildingLevel = GetComponent<BuildingLevel>();
            // BuildingData = GetComponent<BuildingData>();
            //
            // BuildingVisual?.Init(this);
            // BuildingAnimator?.Init(this);
            // BuildingNPC?.Init(this);
            // BuildingHealth?.Init(this);
            // BuildingLevel?.Init(this);
            // BuildingData?.Init(this);
        }

        protected virtual void HandleCompleteEvent()
        {
            var visual = GetBuildingComponent<BuildingVisual>();
            visual.BuildingTransparent(1f);
            _pvc.PlayAnimation();
            visual.SetFloatProperty("_Alpha", 1f);
        }

        public void Building()
        {
            var visual = GetBuildingComponent<BuildingVisual>();
            visual.SetMaterial(visual.VisualMat);

            var buildingData = GetBuildingComponent<BuildingData>().Data;
            StartCoroutine(BuildingRoutine(buildingData.BuildTime.GetSecond()));
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
            return components.OfType<T>().FirstOrDefault();
        }
    }
}