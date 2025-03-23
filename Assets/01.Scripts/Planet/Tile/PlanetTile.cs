using System;
using JMT.Building;
using JMT.Object;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class PlanetTile : TouchableObject
    {
        [field:SerializeField] public TileType TileType { get; set; }
        [field:SerializeField] public MeshRenderer Renderer { get; private set; }
        [field:SerializeField] public MeshFilter Filter { get; private set; }
        [SerializeField] private float _tileHeight;
        
        [SerializeField] private Fog _fog;
        
        private bool canInteraction = true;
        
        private BuildingBase _currentBuilding;
        public BuildingBase CurrentBuilding => _currentBuilding;
        private GameObject TileInteraction;

        public event Action OnBuild;
        public new event Action<PlanetTile> OnClick;

        private void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();
            Filter = GetComponent<MeshFilter>();
            Renderer.material = Instantiate(Renderer.material);
            TileInteraction = transform.GetComponentInChildren<TileInteraction>().gameObject;
            //_tileHeight = UnityEngine.Random.Range(0f, 10f);

            //base.OnClick += OnPointerClickHandler;
        }


        private void Start()
        {
            //SetHeight(_tileHeight);
            _fog.SetFog(false);
        }

        public bool CanBuild()
        {
            return !_fog.IsFogActive || _currentBuilding == null;
        }

        private void SetHeight(float height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        }

        public void Build(BuildingDataSO building)
        {
            if (CanBuild())
            {
                Debug.Log("Build");
                OnBuild?.Invoke();
                _currentBuilding = Instantiate(building.prefab, TileInteraction.transform);
                _currentBuilding.SetBuildingData(building);
                

                RemoveInteraction();
                //_currentBuilding.Build(transform.position + new Vector3(0, 0, 50f));
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
            }
        }

        public void AddInteraction<T>() where T : TileInteraction
        {
            TileInteraction.AddComponent<T>();
        }

        public void RemoveInteraction()
        {
            Destroy(TileInteraction.GetComponent<TileInteraction>());
        }

        public T GetInteraction<T>() where T : TileInteraction
        {
            canInteraction = true;
            return TileInteraction.GetComponent<T>();
        }

        public void RemoveInteractionObject()
        {
            Destroy(TileInteraction.transform.GetChild(0).gameObject);
            canInteraction = false;
        }

        public void OnPointerClickHandler()
        {
            if (!canInteraction) return;
            OnClick?.Invoke(this);
            //UIManager.Instance.NoTouchUI.NoTouchZone.OnClickEvent += () => EdgeEnable(false);
        }

        public void EdgeEnable(bool enable)
        {
            Renderer.material.SetFloat("_IsEdgeOn", enable ? 1 : 0);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<BaseBuilding>(out var building))
            {
                _currentBuilding = building;
            }
        }
    }
}