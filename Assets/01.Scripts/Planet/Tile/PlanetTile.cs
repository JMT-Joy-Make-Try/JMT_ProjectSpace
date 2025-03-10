using System;
using JMT.Building;
using JMT.Planets.Tile.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.Planets.Tile
{
    public class PlanetTile : MonoBehaviour, IPointerClickHandler
    {
        [field:SerializeField] public TileType TileType { get; set; }
        [field:SerializeField] public MeshRenderer Renderer { get; private set; }
        [SerializeField] private float _tileHeight;
        
        private BuildingBase _currentBuilding;

        public event Action OnBuild;
        public event Action<PlanetTile> OnClick;
        public event Action<BuildingBase> OnBuildingValueChanged;

        private void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();
        }

        public bool CanBuild()
        {
            return _currentBuilding == null;
        }

        private void SetHeight(float height)
        {
            transform.localPosition = new Vector3(transform.position.x, transform.position.y + height, _tileHeight);
        }

        public void Build(BuildingBase building)
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
        
        public void DestroyBuilding()
        {
            if (_currentBuilding != null)
            {
                Destroy(_currentBuilding.gameObject);
                _currentBuilding = null;
                OnBuildingValueChanged?.Invoke(null);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
    }
}