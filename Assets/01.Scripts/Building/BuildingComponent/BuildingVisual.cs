using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace JMT.Building.Component
{
    public class BuildingVisual : MonoBehaviour, IBuildingComponent
    {
        [SerializeField] private Material visualMat;
        [SerializeField] private List<MeshRenderer> rendererList;
        [SerializeField] private List<GameObject> buildingLevelObjects;
        
        private GameObject _currentLevelObject;
        
        public BuildingBase Building { get; private set; }
        public Material VisualMat => visualMat;
        public List<MeshFilter> MeshFilters => rendererList.ConvertAll(renderer => renderer.GetComponent<MeshFilter>());

        private void Start()
        {
            visualMat = Instantiate(visualMat);
            for (byte i = 0; i < rendererList.Count; i++)
            {
                if (rendererList[i] != null)
                {
                    rendererList[i].material = Instantiate(rendererList[i].material);
                }
            }

            _currentLevelObject = buildingLevelObjects[0];
        }

        private void OnDestroy()
        {
            Building.GetBuildingComponent<BuildingLevel>().OnLevelChanged -= OnBuildingLevelChanged;
        }

        private void OnBuildingLevelChanged(int level)
        {
            if (buildingLevelObjects == null || buildingLevelObjects.Count == 0)
            {
                Debug.LogWarning("Building level objects are not set or empty.");
                return;
            }
            if (_currentLevelObject == null)
            {
                Debug.LogWarning("Current level object is null.");
                _currentLevelObject = buildingLevelObjects[0];
            }
            
            _currentLevelObject.SetActive(false);
            _currentLevelObject = buildingLevelObjects[level];
            if (_currentLevelObject == null)
            {
                Debug.LogWarning($"Building level object for level {level} is null.");
                return;
            }
            _currentLevelObject.SetActive(true);
        }

        public void SetFloatProperty(string propertyName, float value, bool isAllRendererChange = false)
        {
            visualMat.SetFloat(propertyName, value);
            if (!isAllRendererChange) return;
            for (byte i = 0; i < rendererList.Count; i++)
            {
                if (rendererList[i] != null)
                {
                    rendererList[i].material.SetFloat(propertyName, value);
                }
            }
        }
        
        public void SetShadowCastingMode(ShadowCastingMode mode)
        {
            for (byte i = 0; i < rendererList.Count; i++)
            {
                if (rendererList[i] != null)
                {
                    rendererList[i].shadowCastingMode = mode;
                }
            }
        }

        public void SetMaterial(Material material)
        {
            for (byte i = 0; i < rendererList.Count; i++)
            {
                if (rendererList[i] != null)
                {
                    rendererList[i].material = material;
                }
            }
        }
        
        public void BuildingTransparent(float value, bool isAllRendererChange = false)
        {
            SetFloatProperty("_Alpha", value, isAllRendererChange);
            SetShadowCastingMode(value < 1f ? ShadowCastingMode.Off : ShadowCastingMode.On);
        }

        public void Init(BuildingBase building)
        {
            Building = building;
            Building.GetBuildingComponent<BuildingLevel>().OnLevelChanged += OnBuildingLevelChanged;
        }
    }
}