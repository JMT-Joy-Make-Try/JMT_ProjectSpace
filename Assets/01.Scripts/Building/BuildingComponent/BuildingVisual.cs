using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace JMT.Building.Component
{
    public class BuildingVisual : MonoBehaviour, IBuildingComponent
    {
        [SerializeField] private Material visualMat;
        [SerializeField] private Material visualMat2;
        [SerializeField] private List<MeshRenderer> rendererList;
        [SerializeField] private List<MeshRenderer> rendererList2;
        [SerializeField] private List<GameObject> buildingLevelObjects;
        [SerializeField] private bool _isBeforeLevelVisualActiveFalse = true;
        
        private GameObject _currentLevelObject;
        
        public BuildingBase Building { get; private set; }
        public Material VisualMat => visualMat;
        public List<MeshFilter> MeshFilters => rendererList.ConvertAll(renderer => renderer.GetComponent<MeshFilter>());

        private void Start()
        {
            visualMat = Instantiate(visualMat);
            if (visualMat2 != null)
            {
                visualMat2 = Instantiate(visualMat2);
            }
            SetMaterial(visualMat, rendererList);
            SetMaterial(visualMat2, rendererList2);

            if (buildingLevelObjects == null || buildingLevelObjects.Count == 0)
            {
                Debug.LogWarning("Building level objects are not set or empty.");
                return;
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
            
            if (_isBeforeLevelVisualActiveFalse)
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
            if (visualMat2 != null)
            {
                visualMat2.SetFloat(propertyName, value);
            }
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

        public void SetMaterial(Material material, List<MeshRenderer> rendererL = null)
        {
            if (rendererL == null)
            {
                rendererL = rendererList;
            }
            for (byte i = 0; i < rendererL.Count; i++)
            {
                if (rendererL[i] != null)
                {
                    rendererL[i].material = material;
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
    }
}