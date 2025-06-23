using AYellowpaper.SerializedCollections;
using JMT.Item;
using JMT.Planets.Tile;
using JMT.Sound;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingBuilder : MonoBehaviour, IBuildingComponent
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> _destroyBuildingItems = new SerializedDictionary<ItemSO, int>();
        [field: SerializeField] public SoundPlayer SoundPlayer { get; private set; }
        public BuildingBase Building { get; private set; }
        
        public bool IsBuilding { get; private set; }
        public bool IsBuildingComplete { get; private set; } = false;
        private PVCBuilding _pvc;
        
        public PVCBuilding PVC => _pvc;
        
        public event Action OnCompleteEvent;
        public event Action OnGaugeFullEvent;
        public event Action OnBuildingEvent;
        
        public void Init(BuildingBase building)
        {
            Building = building;
            IsBuilding = false;
            IsBuildingComplete = false;
            OnCompleteEvent += HandleCompleteEvent;
        }

        private void OnDestroy()
        {
            OnCompleteEvent -= HandleCompleteEvent;
            if (_pvc != null)
            {
                _pvc.OnGaugeFull -= HandleGaugeFull;
                _pvc.OnGaugeHold -= HandleGaugeHold;
            }
        }
        
        public void CompleteEventInvoker()
        {
            OnCompleteEvent?.Invoke();
        }
        
        public void SetPVCBuilding(PVCBuilding pvc)
        {
            _pvc = pvc;
            _pvc.OnGaugeFull += HandleGaugeFull;
            _pvc.OnGaugeHold += HandleGaugeHold;
        }

        private void HandleGaugeHold(bool isHold)
        {
            if (isHold)
                SoundPlayer.PlaySound("Building_Sound");
            else
                SoundPlayer.StopSound("Building_Sound");
        }

        private void HandleGaugeFull()
        {
            OnGaugeFullEvent?.Invoke();
        }

        public void BuildBuilding()
        {
            var visual = Building.GetBuildingComponent<BuildingVisual>();
            visual.SetMaterial(visual.VisualMat);
            IsBuilding = true;
            OnBuildingEvent?.Invoke();
        }
        
        protected virtual void HandleCompleteEvent()
        {
            var visual = Building.GetBuildingComponent<BuildingVisual>();
            visual.BuildingTransparent(1f);
            PVC.PlayAnimation();
            SoundPlayer.StopSound("Building_Sound");
            SoundPlayer.PlaySound("Building_Complete");
            IsBuildingComplete = true;
        }
        
        public void DestroyBuilding()
        {
            Building.GetPlanetTile().RemoveInteraction();
            Building.GetPlanetTile().AddInteraction<ItemInteraction>();
            foreach (var items in _destroyBuildingItems)
            {
                Building.GetPlanetTile().GetInteraction<ItemInteraction>().SetItem(items.Key, items.Value);
            }
            
            Destroy(gameObject);
        }
    }
}