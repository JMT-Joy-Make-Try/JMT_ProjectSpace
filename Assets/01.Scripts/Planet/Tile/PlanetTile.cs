using System;
using JMT.Building;
using JMT.Building.Component;
using JMT.Core.Manager;
using System.Collections.Generic;
using UnityEngine;
using JMT.UISystem.Interact;
using JMT.QuestSystem;
using JMT.Agent.Trader;

namespace JMT.Planets.Tile
{
    public class PlanetTile : MonoBehaviour
    {
        [field: SerializeField] public PlanetTile BaseTile { get; private set; }
        [field: SerializeField] public bool IsRocketTile { get; private set; } = false;
        [Tooltip("건물이 건설되기 시작했을 때 일어나는 액션입니다.")]
        public event Action OnBuild;
        public event Action<TileInteraction> OnChangeInteraction;
        public event Action OnPrebuild;

        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        [field: SerializeField] public TileType TileType { get; set; }
        [field: SerializeField] public MeshRenderer Renderer { get; private set; }
        [field: SerializeField] public MeshFilter Filter { get; private set; }
        [field: SerializeField] public QuestPing QuestPing { get; private set; }

        [SerializeField] public Fog Fog;

        [Space] [SerializeField] private List<Texture2D> _textures;

        [SerializeField]private BuildingBase _currentBuilding;
        public BuildingBase CurrentBuilding => _currentBuilding;

        private TileInteraction _tileInteraction;
        public TileInteraction TileInteraction
        {
            get
            {
                if (TileGroup != null && TileGroup.Interaction != null)
                {
                    return TileGroup.Interaction;
                }
                if (_tileInteraction == null)
                {
                    _tileInteraction = GetComponentInChildren<TileInteraction>();
                    if (_tileInteraction == null)
                    {
                        Debug.LogError("TileInteraction component not found in children.");
                    }
                }
                return _tileInteraction;
            }
            private set
            {
                _tileInteraction = value;
            }
        }
        public Transform Pivot { get; private set; }

        private bool canInteraction = true;
        
        private Vector2Int _position;
        public Vector2Int Position => _position;
        
        private Trader _trader;
        public Trader Trader => _trader;

        public PlanetTileGroup TileGroup { get; private set; }

        private void Awake()
        {
            Pivot = transform.Find("Pivot");
            Renderer = GetComponent<MeshRenderer>();
            Filter = GetComponent<MeshFilter>();
            Renderer.material = Instantiate(Renderer.material);
            int randomIndex = UnityEngine.Random.Range(0, _textures.Count);
            Renderer.material.SetTexture("_MainTex", _textures[randomIndex]);
            TileInteraction = transform.GetComponentInChildren<TileInteraction>();
            if (TileGroup == null)
            {
                TileGroup = GetComponentInParent<PlanetTileGroup>();
            }

            if (TileInteraction == null)
            {
                TileInteraction = transform.GetComponentInChildren<TileInteraction>();
            }
        }

        private void Start()
        {
            Vector3 position = transform.position;
            _position = new Vector2Int((int)position.x, (int)position.z);
            TileManager.Instance.RegisterTile((int)position.x, (int)position.z, this);
        }

        public bool CanBuild()
        {
            return /*!Fog.IsFogActive ||*/ _currentBuilding == null;
        }

        public void EnterPreBuildRequirementState()
        {
            var preBuild = ChangeInteraction<PreBuildInteraction>();
            if (TileGroup != null)
            {
                TileGroup.SetInteraction(preBuild);
            }
            preBuild.SetRequiredItems(BuildingManager.Instance.CurrentBuilding.buildingLevel[0].NeedItems, IsRocketTile);
            OnPrebuild?.Invoke();
        }

        public void Build(BuildingDataSO building, PVCBuilding pvc)
        {
            gameObject.layer = LayerMask.NameToLayer("Ground");
            pvc.SetVisualActive(true);
            
            if (_currentBuilding == null)
                _currentBuilding = Instantiate(building.Prefab, TileInteraction.transform);
            _currentBuilding.GetBuildingComponent<BuildingData>().SetBuildingData(building, pvc);

            OnBuild?.Invoke();
            ChangeInteraction<HoldingInteraction>();
        }

        public void DestroyBuilding()
        {
            if (_currentBuilding != null)
            {
                Destroy(_currentBuilding.gameObject);
                _currentBuilding = null;
            }
        }
        
        

        public T AddInteraction<T>() where T : TileInteraction
        {
            TileInteraction = TileInteraction.AddComponent<T>();
            OnChangeInteraction?.Invoke(TileInteraction);
            string interactionName = TileInteraction.GetType().Name.Replace("Interaction", "");

            if (Enum.TryParse<InteractType>(interactionName, out var interactType))
            {
                TileInteraction.SetType(interactType);
            }
            else
            {
                Debug.LogError($"Interaction type {interactionName} is not defined in InteractType enum.");
                TileInteraction.SetType(InteractType.None);
            }

            return TileInteraction as T;
        }

        public void RemoveInteraction()
        {
            Destroy(TileInteraction.GetComponent<TileInteraction>());
        }
        
        public T ChangeInteraction<T>() where T : TileInteraction
        {
            if (TileGroup != null && TileGroup.Interaction != null)
            {
                TileGroup.ChangeInteraction<T>();
                return TileGroup.GetInteraction<T>();
            }
            RemoveInteraction();
            return AddInteraction<T>();
        }
        
        public bool TryGetInteraction<T>(out T interaction) where T : TileInteraction
        {
            interaction = TileInteraction as T;
            if (interaction != null)
            {
                canInteraction = true;
                return true;
            }

            canInteraction = false;
            return false;
        }

        public T GetInteraction<T>() where T : TileInteraction
        {
            if (TileGroup != null && TileGroup.Interaction != null)
            {
                return TileGroup.GetInteraction<T>();
            }
            if (IsTraderOnTile())
            {
                _trader.Interact();
                return null;
            }
            canInteraction = true;
            var interaction = TileInteraction.GetComponent<T>();

            if (interaction == null)
            {
                Debug.LogError($"Can't find interaction of type {typeof(T)}");
                return null;
            }
            
            string interactionName = interaction.GetType().Name.Replace("Interaction", "");

            if (Enum.TryParse<InteractType>(interactionName, out var interactType))
            {
                interaction.SetType(interactType);
            }
            else
            {
                Debug.LogError($"Interaction type {interactionName} is not defined in InteractType enum.");
                interaction.SetType(InteractType.None);
            }

            return interaction;
        }

        public void EdgeEnable(bool enable)
        {
            Renderer.material.SetFloat("_IsEdgeOn", enable ? 1 : 0);
        }

        public void TestBuild(BuildingDataSO building)
        {
            DestroyBuilding();
            _currentBuilding = Instantiate(building.Prefab, TileInteraction.transform);
            _currentBuilding.GetBuildingComponent<BuildingVisual>().BuildingTransparent(0.5f, true);
        }
        public void SetColor(Color color)
        {
            Renderer.material.SetColor(BaseColor, color);
        }

        public void SetTrader(Trader trader)
        {
            _trader = trader;
        }

        public bool IsTraderOnTile()
        {
            return _trader != null;
        }
    }
}