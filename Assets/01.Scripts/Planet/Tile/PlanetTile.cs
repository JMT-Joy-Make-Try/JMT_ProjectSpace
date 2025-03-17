using System;
using JMT.Building;
using JMT.Object;
using JMT.Planets.Tile.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.Planets.Tile
{
    public class PlanetTile : TouchableObject
    {
        [field:SerializeField] public TileType TileType { get; set; }
        [field:SerializeField] public MeshRenderer Renderer { get; private set; }
        [field:SerializeField] public MeshFilter Filter { get; private set; }
        [SerializeField] private float _tileHeight;
        
        private BuildingBase _currentBuilding;
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

            base.OnClick += OnPointerClickHandler;
        }


        private void Start()
        {
            //SetHeight(_tileHeight);
        }

        public bool CanBuild()
        {
            return _currentBuilding == null;
        }

        private void SetHeight(float height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        }

        public void Build(BuildingBase building)
        {
            if (CanBuild())
            {
                Debug.Log("Build");
                OnBuild?.Invoke();
                _currentBuilding = Instantiate(building, TileInteraction.transform);
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

        public void ChangeInteraction<T>() where T : TileInteraction
        {
            Destroy(TileInteraction.GetComponent<TileInteraction>());
            TileInteraction.AddComponent<T>();
        }
        
        public T GetInteraction<T>() where T : TileInteraction
        {
            return TileInteraction.GetComponent<T>();
        }

        public void RemoveInteractionObject()
        {
            Destroy(TileInteraction.transform.GetChild(0).gameObject);
        }

        public void OnPointerClickHandler()
        {
            OnClick?.Invoke(this);
            EdgeEnable(true);
        }

        public void EdgeEnable(bool enable)
        {
            Renderer.material.SetFloat("_IsEdgeOn", enable ? 1 : 0);
        }
    }
}