using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.Item;
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
        #region Building Component
        public List<IBuildingComponent> components = new List<IBuildingComponent>();
        
        private Dictionary<Type, IBuildingComponent> _componentLookup = new Dictionary<Type, IBuildingComponent>();
        
        [field: SerializeField] public SoundPlayer SoundPlayer { get; private set; }
        #endregion
        
        public bool IsBuilding { get; private set; }
        public Action OnCompleteEvent;
        
        [SerializeField] private float _fuelAmount;
        
        [Header("Destroy Building")]
        [SerializeField] private SerializedDictionary<ItemSO, int> _destroyBuildingItems = new SerializedDictionary<ItemSO, int>();
        
        public float FuelAmount
        {
            get => _fuelAmount;
            set => _fuelAmount = value;
        }
        
        protected bool _isWorking;
        private PVCBuilding _pvc;
        
        public PVCBuilding PVC => _pvc;
        
        protected virtual void Awake()
        {
            InitBuildingComponents();
            BuildingManager.Instance.AddBuilding(this);
            
            OnCompleteEvent += HandleCompleteEvent;
            GetBuildingComponent<BuildingHealth>().OnBuildingBroken += HandleBroken;
        }

        private void HandleBroken()
        {
            
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
            GetBuildingComponent<BuildingHealth>().OnBuildingBroken -= HandleBroken;
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
            SoundPlayer.StopSound("Building_Sound");
            SoundPlayer.PlaySound("Building_Complete");
        }

        public void Building()
        {
            var visual = GetBuildingComponent<BuildingVisual>();
            visual.SetMaterial(visual.VisualMat);

            var buildingData = GetBuildingComponent<BuildingData>().Data;
            StartCoroutine(BuildingRoutine(buildingData.buildingLevel[0].BuildTime.GetSecond()));
            SoundPlayer.PlaySound("Building_Sound");
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

        public void SetLayer(string layerName)
        {
            int layer = LayerMask.NameToLayer(layerName);
            if (layer == -1)
            {
                Debug.LogError($"Layer '{layerName}' not found.");
                return;
            }

            foreach (Transform child in transform)
            {
                child.gameObject.layer = layer;
            }
        }

        public void DestroyBuilding()
        {
            foreach (var items in _destroyBuildingItems)
            {
                GameUIManager.Instance.InventoryCompo.AddItem(items.Key, items.Value);
            }
            GetPlanetTile().RemoveInteraction();
            GetPlanetTile().AddInteraction<NoneInteraction>();
            Destroy(gameObject);
        }

    }
}