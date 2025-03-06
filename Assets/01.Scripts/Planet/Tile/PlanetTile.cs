using System;
using JMT.Building;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.Planets.Tile
{
    public abstract class PlanetTile : MonoBehaviour, IPointerClickHandler
    {
        [field:SerializeField] public TileType TileType { get; set; }
        [field:SerializeField] public MeshRenderer Renderer { get; private set; }
        [SerializeField] protected float _tileHeight;
        
        protected BuildingBase _currentBuilding;

        public event Action OnBuild;
        public event Action<PlanetTile> OnClick;
        public event Action<BuildingBase> OnBuildingValueChanged;

        protected virtual void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();
        }

        public virtual bool CanBuild()
        {
            return false;
        }

        public virtual void Build(BuildingBase building)
        {
            if (CanBuild())
            {
                Debug.Log("Build");
                OnBuild?.Invoke();
                OnBuildingValueChanged?.Invoke(building);
                _currentBuilding = building;
            }
            else
            {
                Debug.Log("Can't Build");
            }
        }
        
        public virtual void DestroyBuilding()
        {
            if (_currentBuilding != null)
            {
                Destroy(_currentBuilding.gameObject);
                _currentBuilding = null;
                OnBuildingValueChanged?.Invoke(null);
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
    }
}