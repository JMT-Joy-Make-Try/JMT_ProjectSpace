using System;
using JMT.Building;
using JMT.Building.Component;
using System.Collections.Generic;
using UnityEngine;
using JMT.UISystem.Interact;
using JMT.QuestSystem;

namespace JMT.Planets.Tile
{
    public class PlanetTile : MonoBehaviour
    {
        public event Action OnBuild;
        [field: SerializeField] public TileType TileType { get; set; }
        [field: SerializeField] public MeshRenderer Renderer { get; private set; }
        [field: SerializeField] public MeshFilter Filter { get; private set; }
        [field: SerializeField] public QuestPing QuestPing { get; private set; }

        [SerializeField] public Fog Fog;

        [Space] [SerializeField] private List<Texture2D> _textures;

        private BuildingBase _currentBuilding;
        public BuildingBase CurrentBuilding => _currentBuilding;
        public GameObject TileInteraction;

        private bool canInteraction = true;
        
        private TileList _tileList;

        private void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();
            Filter = GetComponent<MeshFilter>();
            _tileList = GetComponentInParent<TileList>();
            Renderer.material = Instantiate(Renderer.material);
            int randomIndex = UnityEngine.Random.Range(0, _textures.Count);
            Renderer.material.SetTexture("_MainTex", _textures[randomIndex]);
            TileInteraction = transform.GetComponentInChildren<TileInteraction>().gameObject;
        }

        public bool CanBuild()
        {
            return !Fog.IsFogActive || _currentBuilding == null;
        }

        public void Build(BuildingDataSO building, PVCBuilding pvc)
        {
            if (CanBuild())
            {
                Debug.Log("Build");
                OnBuild?.Invoke();
                PVCBuilding pvcBuilding = Instantiate(pvc, TileInteraction.transform);
                if (_currentBuilding == null)
                    _currentBuilding = Instantiate(building.Prefab, TileInteraction.transform);
                _currentBuilding.GetBuildingComponent<BuildingData>().SetBuildingData(building, pvcBuilding);


                RemoveInteraction();
                AddInteraction<ProgressInteraction>();
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
        
        public bool TryGetInteraction<T>(out T interaction) where T : TileInteraction
        {
            interaction = TileInteraction.GetComponent<T>();
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
            canInteraction = true;
            var interaction = TileInteraction.GetComponent<T>();
            switch (interaction)
            {
                case BuildingInteraction:
                    interaction.SetType(InteractType.Building);
                    break;
                case ItemInteraction:
                    interaction.SetType(InteractType.Item);
                    break;
                case StationInteraction:
                    interaction.SetType(InteractType.Station);
                    break;
                case NoneInteraction:
                    interaction.SetType(InteractType.None);
                    break;
                case ProgressInteraction:
                    interaction.SetType(InteractType.Progress);
                    break;
                case ZeoliteInteraction:
                    interaction.SetType(InteractType.Zeolite);
                    break;
                case VillageInteraction:
                    interaction.SetType(InteractType.Village);
                    break;
                case LaboratoryInteraction:
                    interaction.SetType(InteractType.Laboratory);
                    break;
            }

            return interaction;
        }

        public void EdgeEnable(bool enable)
        {
            Renderer.material.SetFloat("_IsEdgeOn", enable ? 1 : 0);
            if (Fog.IsFogActive)
            {
                _tileList.LineRenderer.enabled = enable;
            }
        }

        public void TestBuild(BuildingDataSO building)
        {
            DestroyBuilding();
            _currentBuilding = Instantiate(building.Prefab, TileInteraction.transform);
            _currentBuilding.GetBuildingComponent<BuildingVisual>().BuildingTransparent(0.5f);
        }
    }
}